namespace PlyQor.Engine.Components.Validation.Internals
{
    using Newtonsoft.Json;
    using PlyQor.Engine.Core;
    using PlyQor.Models;
    using PlyQor.Resources;
    using System;
    using System.Collections.Generic;

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
                    throw new PlyQorException(StatusCode.ERRMALFORM);
                }

                return JsonConvert.DeserializeObject<Dictionary<string, string>>(input);
            }
            catch (Exception ex)
            {
                throw new PlyQorException(StatusCode.ERRMALFORM, ex);
            }
        }

        /// <summary>
        /// Check if token is valid.
        /// </summary>
        public static bool CheckToken(string container, string token)
        {
            if (Configuration.Tokens.TryGetValue(container.ToUpper(), out List<string> tokens))
            {
                if (tokens.Contains(token))
                {
                    return true;
                }

                throw new PlyQorException(StatusCode.ERRBLOCK);
            }

            throw new PlyQorException(StatusCode.ERRBLOCK);
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

            throw new PlyQorException(StatusCode.ERR033);
        }
    }
}
