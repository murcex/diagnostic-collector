using System.Text.RegularExpressions;

namespace Implements.Configuration.Internal
{
    public class Deserializer
    {
        /// <summary>
        /// Core Deserializer process.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, string>> Execute(List<string> lines)
        {
            Dictionary<string, Dictionary<string, string>> elements = new();

            Dictionary<string, string> components = new();

            /// <summary>
            /// Flag used by the rule engine to determine if a Tag has been identified.
            /// </summary>
            bool elementFilterSwitch = false;

            /// <summary>
            /// The current Tag name.
            /// </summary>
            string currentElementName = string.Empty;

            try
            {
                int lineCounter = 1;
                int lineCount = lines.Count;

                foreach (var line in lines)
                {
                    var isCommentLine = CheckLineForComment(line);

                    if (elementFilterSwitch
                        && line != string.Empty
                        && !isCommentLine)
                    {
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            elementFilterSwitch = true;

                            var ElementName = CleanElementHeader(line);

                            // close perviously open element
                            elements.Add(currentElementName, components); // TODO: why?

                            components = new();

                            currentElementName = ElementName;
                        }
                        else
                        {
                            var result = GetKeyValuePair(line);

                            components.Add(result.key, result.value);
                        }
                    }
                    else if (!isCommentLine)
                    {
                        if (line.Contains("[") && line.Contains("]"))
                        {
                            elementFilterSwitch = true;

                            var elementName = CleanElementHeader(line);

                            currentElementName = elementName;
                        }
                        else
                        {
                            // no hit
                        }
                    }
                    else
                    {
                        if (isCommentLine)
                        {
                            // comment
                        }
                        else
                        {
                            // unknown
                        }
                    }

                    // check for last line -- ensure current open tag is added to collection
                    if (lineCounter == lineCount)
                    {
                        elements.Add(currentElementName, components);
                    }
                    else
                    {
                        lineCounter++;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Deserializer Exception [Deserializer].[Execute()]: Rule Engine Error: {e}");
            }

            return elements;
        }

        /// <summary>
        /// Clean the Tag of brackets.
        /// </summary>
        /// <param name="rawTag"></param>
        /// <returns></returns>
        private string CleanElementHeader(string rawTag)
        {
            var tagName = rawTag.Replace("[", "");
            tagName = tagName.Replace("]", "");

            return tagName;
        }

        /// <summary>
        /// Check if line is a comment.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool CheckLineForComment(string line)
        {
            var compactedLine = Regex.Replace(line, @"\s+", "");

            if (compactedLine.Contains(";") && compactedLine[0] == ';')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private (string key, string value) GetKeyValuePair(string line)
        {
            bool SecondValueSwitch = false;
            string firstValue = string.Empty;
            string secondValue = string.Empty;

            // scan foreach char in line
            foreach (var chr in line)
            {
                if (!SecondValueSwitch)
                {
                    if (chr == '=')
                    {
                        SecondValueSwitch = true;
                    }
                    else
                    {
                        if (firstValue == string.Empty)
                        {
                            firstValue = chr.ToString();
                        }
                        else
                        {
                            firstValue = string.Concat(firstValue, chr.ToString());
                        }
                    }
                }
                else
                {
                    if (chr != '"')
                    {
                        if (secondValue == string.Empty)
                        {
                            secondValue = chr.ToString();
                        }
                        else
                        {
                            secondValue = string.Concat(secondValue, chr.ToString());
                        }
                    }
                }
            }

            return (firstValue, secondValue);
        }
    }
}
