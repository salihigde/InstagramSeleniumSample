using OpenQA.Selenium.PhantomJS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramSeleniumSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var insta = new SeleniumInstagram<PhantomJSDriver>("YOUR_INSTAGRAM_USER_NAME","YOUR_INSTAGRAM_PASSWORD");

            insta.Login();

            var result = insta.FollowUser("INSTAGRAM_USER_TO_FOLLOW");

            Console.WriteLine(result);
            Console.WriteLine();
        }
    }
}
