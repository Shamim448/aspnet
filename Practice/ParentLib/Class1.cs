namespace ParentLib
{
    public class Class1
    {
        private readonly IPrint _injectedClass;
        public Class1(IPrint injectedClass) { 
            _injectedClass = injectedClass;
        }
        public void PrintSomething()
        {
            Console.WriteLine("This is parent class");
            string  status = "dpendent class";
            _injectedClass.Print(status);
        }
    }
}