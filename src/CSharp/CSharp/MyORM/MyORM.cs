using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

public class MyORM<G, T> where T : IEntity<G>
{
    private readonly string _connectionString;

    public MyORM(string connectionString)
    {
        _connectionString = connectionString;
    }

    
    public void Insert(T item)
    {
        using var connection = new SqlConnection(_connectionString);
        connection.Open();
        //using var transaction = connection.BeginTransaction();
        try
        {
            Insert(item, connection/*, transaction*/);

           // transaction.Commit();
        }
        catch (Exception)
        {
           // transaction.Rollback();
            throw;
        }

    }
    private void Insert(object obj, SqlConnection connection/*, SqlTransaction transaction*/)
    {
        var type = obj.GetType();
        var tableName = type.Name;
        var properties = type.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                             //.Where(p => p.CanRead && p.CanWrite && p.GetGetMethod()?.IsVirtual == false);

        var primaryKeyProperty = properties.FirstOrDefault(p => p.Name == "Id");
        if (primaryKeyProperty == null)
        {
            throw new ArgumentException($"Type '{type.Name}' does not have a primary key property named 'Id'");
        }

        var primaryKeyValue = primaryKeyProperty.GetValue(obj);
        if (primaryKeyValue == null)
        {
            throw new ArgumentException($"Primary key value for type '{type.Name}' cannot be null");
        }

        var commandText = $"INSERT INTO [{tableName}] ({string.Join(",", properties.Select(p => $"[{p.Name}]"))}) " +
            $"VALUES ({string.Join(",", properties.Select(p => $"@{p.Name}"))});";

        using var command = new SqlCommand(commandText, connection/*, transaction*/);

        foreach (var property in properties)
        {
            var value = property.GetValue(obj);
            if (value != null)
            {
                if (property.PropertyType.IsValueType || property.PropertyType == typeof(string))
                {
                    command.Parameters.AddWithValue($"@{property.Name}", value);
                }
                // Check if the property is a foreign key reference
                else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    // Get the foreign key object's ID property value
                    var foreignKeyProperty = property.GetValue(obj).GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                        .FirstOrDefault(p => p.Name == $"{type.Name}Id");
                    
                    if (foreignKeyProperty != null)
                    {
                        foreignKeyProperty.SetValue(value, primaryKeyValue);
                    }


                    // Set the foreign key property's value to the ID of the foreign key object

                    Insert(value, connection/*, transaction*/);
                }
                else if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    var elementType = property.PropertyType.GetGenericArguments()[0];
                    var subItems = ((IEnumerable<object>)value).ToList();
                    if (subItems.Any())
                    {
                        var foreignKeyProperty = elementType.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                                 .FirstOrDefault(p => p.Name == $"{type.Name}Id");
                        if (foreignKeyProperty == null)
                        {
                            throw new ArgumentException($"Type '{elementType.Name}' does not have a foreign key property named '{type.Name}Id'");
                        }
                        foreach (var subItem in subItems)
                        {
                            foreignKeyProperty.SetValue(subItem, primaryKeyValue);
                            Insert(subItem, connection/*, transaction*/);
                        }
                    }

                }
                else
                {
                    Insert(value, connection/*, transaction*/);
                }
            }
        }//end foreach

        command.ExecuteNonQuery();
    }//end Insert
    

    public void Inserts(T item)
    {
        // Get the type of the item being inserted
        var itemType = typeof(T);

        // Get the properties of the item being inserted
        var properties = itemType.GetProperties();

        // Create a new SqlConnection object to connect to the database
        using (var connection = new SqlConnection(_connectionString))
        {
            // Open the database connection
            connection.Open();

            // Begin a new transaction
            var transaction = connection.BeginTransaction();

            try
            {
                // Insert the item into the database
                InsertItem(item, connection, transaction);

                // Commit the transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Roll back the transaction if an exception is thrown
                transaction.Rollback();
                throw ex;
            }
        }
    }

    private void InsertItem(object item, SqlConnection connection, SqlTransaction transaction)
    {
        // Get the type of the item being inserted
        var itemType = item.GetType();

        // Get the properties of the item being inserted
        var properties = itemType.GetProperties();

        // Create a new SqlCommand object to execute SQL statements
        using (var command = new SqlCommand())
        {
            // Set the SqlCommand object's Connection and Transaction properties
            command.Connection = connection;
            command.Transaction = transaction;

            // Iterate through the properties of the item being inserted
            foreach (var property in properties)
            {
                // Check if the property is a foreign key reference
                if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    // Get the foreign key object's ID property value
                    var foreignKeyId = property.GetValue(item).GetType().GetProperty("Id").GetValue(property.GetValue(item));

                    // Set the foreign key property's value to the ID of the foreign key object
                    property.SetValue(item, foreignKeyId);
                }

                // Check if the property is a List<T> object
                if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(List<>))
                {
                    // Get the list object's item type
                    var listItemType = property.PropertyType.GetGenericArguments()[0];

                    // Check if the list item is a foreign key reference
                    if (listItemType.IsClass && listItemType != typeof(string))
                    {
                        // Get the foreign key ID property name
                        var foreignKeyIdPropertyName = $"{listItemType.Name}Id";

                        // Get the foreign key ID property value for each item in the list
                        var list = property.GetValue(item) as IList;
                        foreach (var listItem in list)
                        {
                            var foreignKeyId = listItem.GetType().GetProperty("Id").GetValue(listItem);
                            listItem.GetType().GetProperty(foreignKeyIdPropertyName).SetValue(listItem, foreignKeyId);
                        }
                    }
                }

                // Set the SqlCommand object's CommandText property to an INSERT statement for the current property
                command.CommandText = $"INSERT INTO {itemType.Name} ({property.Name}) VALUES (@{property.Name})";

                // Set the SqlCommand object's Parameters property to a SqlParameter object with the current property's value
                command.Parameters.Clear();
                command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(item) ?? DBNull.Value);

                // Execute the SQL statement
                command.ExecuteNonQuery();
            }
        }
    }





}//end orm






















//public class MyORM<G, T> where T : IIdBase<G>
//{
//    public readonly string _connectionString;
//    public readonly DataUtility _dataUtility;
//    public string DbConnection = "Server=.\\SQLEXPRESS;Database=Aspnetb8;User Id=aspnetb8;Password=123456;";
//    public MyORM(string connectionString)
//    {
//        _connectionString = connectionString;
//        _dataUtility = new DataUtility(DbConnection);

//    }

//    Type type = typeof(T);
//    string tableName = typeof(T).Name;
//    //Insert value
//    public void Insert(T entity)
//    {
//        #region Insert_Operation
//        //get object info
//        EntityInfo entityInfo = new EntityInfo(entity);
//        var tablename = entityInfo.GetObjectName();
//        string columnName = entityInfo.GetColumn(); 
//        string parametersName = entityInfo.GetParameters(); 
//        //sql query for parametarized Insert
//        string sql = $"Insert into {tablename} ({columnName}) Values({parametersName})";
//        //call sqlconnection and open connection 
//        SqlConnection connection = new SqlConnection(_connectionString);
//        //instialize sqlcommend it takes a sql query and a sqlconnection
//        SqlCommand cmd = new SqlCommand(sql, connection);
//        //initialize value in parameters
//        PropertyInfo[] properties = entityInfo.GetProperties();

//        foreach (var property in properties)
//        {
//            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
//            //check value get or not

//        }
//        foreach (var property in properties)
//        {
//            var value = property.GetValue(entity);
//            if (property.PropertyType.IsGenericType || value is IList)
//            {
//                IList list = (IList)value;
//                if (list != null)
//                {
//                    foreach(var item in list)
//                    {
//                       // Insert(T item);
//                    }
//                }
//            }
//        }
//        try
//        {
//            if (connection.State != System.Data.ConnectionState.Open)
//            {
//                connection.Open();
//                cmd.ExecuteNonQuery();
//                Console.WriteLine("Record Insert Successfully");
//            }
//        }
//        catch(SqlException ex)
//        {
//            Console.WriteLine("Error Generated. Details: " + ex.ToString());
//        }
//        finally { 
//            connection.Close(); 
//        }

//        #endregion
//    }
//    public void Update(T entity) {
//        EntityInfo entityInfo = new EntityInfo(entity);
//        string columnName = string.Join(", ", entityInfo.GetProperties().Select(p => $"{p.Name} = @{p.Name}"));
//        string sql = $"Update {tableName} set {columnName} where Title = @Title";
//        SqlConnection connection = new SqlConnection(_connectionString);
//        SqlCommand cmd = new SqlCommand(sql, connection);
//        //initialize value in parameters
//        foreach (var property in entityInfo.GetProperties())
//        {
//            cmd.Parameters.AddWithValue($"@{property.Name}", property.GetValue(entity));
//            //check value get or not
//            var value = property.GetValue(entity);
//        }
//        try
//        {
//            if (connection.State != System.Data.ConnectionState.Open)
//            {
//                connection.Open();
//                cmd.Parameters.AddWithValue("@Id", entity.Id);
//                cmd.ExecuteNonQuery();
//                Console.WriteLine("Record Insert Successfully");
//            }
//        }
//        catch (SqlException ex)
//        {
//            Console.WriteLine("Error Generated. Details: " + ex.ToString());
//        }
//        finally
//        {
//            connection.Close();
//        }
//    }
//    public void Delete(T entity)
//    {
//        EntityInfo entityInfo = new EntityInfo(entity);
//      // string s = $"ALTER TABLE {tableName} DROP COLUMN {entityInfo.GetColumn()}";
//        string sql = $"Drop Table {entityInfo.GetObjectName()}";
//        _dataUtility.ExecuteCommand(sql);
//    }
//    public void Delete(G id) {
//        #region Delete_By _ID
//        string sql = $"Delete From {tableName} Where Id = {id}";
//        _dataUtility.ExecuteCommand(sql);

//        #endregion
//    }
//    public void GetById(G id)    {
//        #region Print_Value_By_Id
//        //sql query for select all data
//        string sql = $"Select * from {tableName} Where Id = {id}";
//        _dataUtility.ReadData(sql);
//        #endregion
//    }
//    public void GetAll() {
//        #region Get_All_Table_Data
//        //sql query for select all data
//        string sql = $"Select * from {tableName}";
//        _dataUtility.ReadData(sql);       
//        #endregion
//    }
//}
