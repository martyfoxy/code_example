namespace Code.Parameters
{
    public static class ParametersOperations
    {
        public static float Add(float arg1, float arg2)
        {
            return arg1 + arg2;
        }
        
        public static float Multiply(float arg1, float arg2)
        {
            return arg1 * arg2;
        }
        
        public static int Add(int arg1, int arg2)
        {
            return arg1 + arg2;
        }
        
        public static int Multiply(int arg1, int arg2)
        {
            return arg1 * arg2;
        }
        
        public static bool Add(bool arg1, bool arg2)
        {
            return arg1 || arg2;
        }
        
        public static bool Multiply(bool arg1, bool arg2)
        {
            return arg1 && arg2;
        }
    }
}