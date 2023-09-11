GO
/****** Object:  StoredProcedure [dbo].[GetCourseEnrollments]    Script Date: 09/07/2023 8:04:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER   PROCEDURE [dbo].[GetCourseEnrollments]
@PageIndex int,
@PageSize int , 
@OrderBy nvarchar(50),
@CourseName nvarchar(250) = '%',
@UserName nvarchar(250) = '%',
@EnrollmentDateFrom datetime = null,
@EnrollmentDateTo datetime = null,
@Total int output,
@TotalDisplay int output

AS
BEGIN
	Declare @sql nvarchar(2000);
	Declare @countsql nvarchar(2000);
	Declare @paramList nvarchar(MAX); 
	Declare @countparamList nvarchar(MAX);
	
	SET NOCOUNT ON;

	Select @Total = count(*) from UserCourse;
	SET @countsql = 'select @TotalDisplay = count(*) from UserCourse us inner join 
					Courses c on us.CourseId = c.Id inner join
					Users u on us.UserId = u.Id  where 1 = 1 ';
	
	IF @CourseName IS NOT NULL
	SET @countsql = @countsql + ' AND c.Name LIKE ''%'' + @xCourseName + ''%''' 

	IF @UserName IS NOT NULL
	SET @countsql = @countsql + ' AND u.Name LIKE ''%'' + @xStudentName + ''%''' 

	IF @EnrollmentDateFrom IS NOT NULL
	SET @countsql = @countsql + ' AND EnrollDate >= @xEnrollmentDateFrom'

	IF @EnrollmentDateTo IS NOT NULL
	SET @countsql = @countsql + ' AND EnrollDate <= @xEnrollmentDateTo' 


	SET @sql = 'select c.Name as CourseName, u.Name as UserName, EnrollDate from UserCourse us inner join 
				Courses c on us.CourseId = c.Id inner join
				Users u on us.UserId = u.Id where 1 = 1 '; 

	IF @CourseName IS NOT NULL
	SET @sql = @sql + ' AND c.Name LIKE ''%'' + @xCourseName + ''%''' 

	IF @UserName IS NOT NULL
	SET @sql = @sql + ' AND u.Name LIKE ''%'' + @xStudentName + ''%''' 

	IF @EnrollmentDateFrom IS NOT NULL
	SET @sql = @sql + ' AND EnrollDate >= @xEnrollmentDateFrom'

	IF @EnrollmentDateTo IS NOT NULL
	SET @sql = @sql + ' AND EnrollDate <= @xEnrollmentDateTo' 

	SET @sql = @sql + ' Order by '+@OrderBy+' OFFSET @PageSize * (@PageIndex - 1) 
	ROWS FETCH NEXT @PageSize ROWS ONLY';

	SELECT @countparamlist = '@xCourseName nvarchar(250),
		@xUserName nvarchar(250),
		@xEnrollmentDateFrom datetime,
		@xEnrollmentDateTo datetime,
		@TotalDisplay int output' ;

	exec sp_executesql @countsql , @countparamlist ,
		@CourseName,
		@UserName,
		@EnrollmentDateFrom,
		@EnrollmentDateTo,
		@TotalDisplay = @TotalDisplay output;

	SELECT @paramlist = '@xCourseName nvarchar(250),
		@xUserName nvarchar(250),
		@xEnrollmentDateFrom datetime,
		@xEnrollmentDateTo datetime,
		@PageIndex int,
		@PageSize int';

	exec sp_executesql @sql , @paramlist ,
		@CourseName,
		@UserName,
		@EnrollmentDateFrom,
		@EnrollmentDateTo,
		@PageIndex,
		@PageSize;

	print @countsql;
	print @sql;
	
END