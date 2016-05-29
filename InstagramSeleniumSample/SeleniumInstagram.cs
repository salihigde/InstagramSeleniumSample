using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstagramSeleniumSample
{
    public class SeleniumInstagram<TWebDriver> where TWebDriver : IWebDriver, new()
    {
        TWebDriver driver;
        private string _userName;
        private string _password;
        public SeleniumInstagram(string userName, string password)
        {
            driver = new TWebDriver();

            //lets selenium to wait 5 seconds until finding the specific dom element.
            driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(5));
            driver.Manage().Timeouts().SetScriptTimeout(TimeSpan.FromSeconds(5));

            this._userName = userName;
            this._password = password;
        }

        public void Login()
        {
            driver.Navigate().GoToUrl("https://instagram.com/accounts/login/");

            System.Threading.Thread.Sleep(3000);

            var userName = driver.FindElement(By.Id("react-root")).FindElement(By.Name("username"));
            var pass = driver.FindElement(By.Id("react-root")).FindElement(By.Name("password"));
            var button = driver.FindElement(By.Id("react-root")).FindElement(By.TagName("button"));

            userName.SendKeys(_userName);
            pass.SendKeys(_password);

            button.Click();
        }

        public string FollowUser(string userName)
        {
            var userProfileUrl = string.Format("http://www.instagram.com/{0}", userName);

            driver.Navigate().GoToUrl(userProfileUrl);

            if (driver.PageSource.Contains("error-container"))
            {
                return "Page Not Found";
            }

            if (!driver.PageSource.Contains("_kenyh _o0442"))
            {
                return "Follow button not found";
            }

            var button = driver.FindElement(By.Id("react-root")).FindElement(By.ClassName("_aj7mu")); // find the button from css class name

            button.Click();

            System.Threading.Thread.Sleep(2000);

            if (button.GetAttribute("class").Contains("_r4e4p")) // following class name
            {
                return "User is been followed successfully";
            }
            else if (button.GetAttribute("class").Contains("_96gf6")) // private account followed class name
            {
                return "Private account is followed successfully";
            }
            else
            {
                return "Error occured";
            }
        }
    }
}
