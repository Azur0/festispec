using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Festispec.DesktopApplication.DesktopControllers
{
    public static class Validators
    {

        // NotEmpty
        // MaxLength:int
        // Email
        // PostalCode
		// MinLength
        public static object Check(object valueToCheck, string name, string _checkers)
        {
            List<string> checkers = _checkers.Split("|").ToList();

            checkers.ForEach(check =>
            {
                if (check.Contains("NotEmpty"))
                {
                    if ((valueToCheck == null) || valueToCheck.ToString().Length == 0)
                    {
                        throw new Exception($"{name} mag niet leeg zijn");
                    }
                }

                if (check.Contains("MixLength"))
                {
                    int value = Int32.Parse(check.Split(":")[1]);

                    if (valueToCheck.ToString().Length < value)
                    {
                        throw new Exception($"{name} moet langer zijn dan {value} characters");
                    }
                }

                if (check.Contains("MaxLength"))
                {
                    int value = Int32.Parse(check.Split(":")[1]);

                    if (valueToCheck.ToString().Length > value)
                    {
                        throw new Exception($"{name} mag niet langer zijn dan {value} characters");
                    }
                }


				if(check.Contains("ExactLength"))
				{
					int value = Int32.Parse(check.Split(":")[1]);

					if(valueToCheck.ToString().Length != value)
					{
						throw new Exception($"{name} moet exact {value} characters bevatten");
					}
				}
				

				if (check.Contains("Email"))

                {
					if (valueToCheck.ToString().Contains("@") == false)
                    {
                        throw new Exception($"{name} is geen geldig email adres");
                    }
                }

                if (check.Contains("PostalCode"))
                {

                    if (Validators.MatchesRegex(valueToCheck.ToString(), @"\d{4} ?[a-zA-Z]{2}") == false)

                    {
                        throw new Exception($"{name} is geen geldig postcode");
                    }
                }
            });

            return valueToCheck;
        }

        private static bool MatchesRegex(string text, string regex)
        {
            return Regex.Match(text, regex, RegexOptions.IgnoreCase).Success;
        }
    }
}
