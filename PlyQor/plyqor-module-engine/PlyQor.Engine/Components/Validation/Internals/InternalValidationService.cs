namespace PlyQor.Engine.Components.Validation.Internals
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using PlyQor.Models;
    using PlyQor.Resources;
    using PlyQor.Engine.Core;

    public class InternalValidationService
    {

        /// <summary>
        /// Convert json string to dictionary.
        /// </summary>
        public static Dictionary<string, string> GenerateDictionary(string input)
        {
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    throw new JavelinException(StatusCode.ERRMALFORM);
                }

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(input);
            }
            catch (Exception ex)
            {
                throw new JavelinException(StatusCode.ERRMALFORM, ex);
            }
        }

        /// <summary>
        /// Check if token is valid.
        /// </summary>
        public static bool CheckToken(string collection, string token)
        {
            if (Configuration.Tokens.TryGetValue(collection, out List<string> tokens))
            {
                if (tokens.Contains(token))
                {
                    return true;
                }

                throw new JavelinException(StatusCode.ERRBLOCK);
            }

            throw new JavelinException(StatusCode.ERRBLOCK);
        }

        /// <summary>
        /// Check if operation is valid.
        /// </summary>
        public static bool CheckOperation(string operation)
        {
            if (Configuration.Operations.Contains(operation))
            {
                return true;
            }

            throw new JavelinException(StatusCode.ERR033);
        }
    }
}
