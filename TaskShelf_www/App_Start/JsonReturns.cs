using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskShelf_www.App_Start
{
    public class JsonReturns
    {
        public static object Redirect(string url)
        {
            return new
            {
                Type = "Redirect",
                Data = new
                {
                    Url = url
                }
            };
        }

        public static object Error(string message)
        {
            return new
            {
                Type = "Error",
                Data = new
                {
                    Message = message
                }
            };
        }

        public static object Info(string message)
        {
            return new
            {
                Type = "Info",
                Data = new
                {
                    Message = message
                }
            };
        }

        public static object ValidationError(string property, string message)
        {
            return new
            {
                Type = "ValidationError",
                Data = new
                {
                    Property = property,
                    Message = message
                }
            };
        }
    }
}