﻿namespace _0_Framework.Application
{
    public static class CustomMethods
    {
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
    }
}
