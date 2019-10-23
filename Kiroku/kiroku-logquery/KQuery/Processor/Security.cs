namespace KQuery
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.Net;

    class Security
    {
        private Dictionary<string, string> firewallRules;

        private Dictionary<string, string> accessKeys;

        private enum SecurityState
        {
            Disabled,
            Granted,
            Denied
        }

        public static void SetFirewallRules()
        {
            // Recycle firewall rule dictionary
        }

        public static void SetAccessKeys()
        {
            // Recycle access key dictionary
        }

        /// <summary>
        /// Evaluate request for security access, by IP Address or Key.
        /// </summary>
        /// <param name="req"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool Check(HttpRequest req, string key = null)
        {
            bool securityCheck;
            SecurityState firewallCheck;
            SecurityState accessKeyCheck;
            IPAddress address;

            // Check Firewall
            try
            {
                address = req.HttpContext.Request.HttpContext.Connection.RemoteIpAddress;

                //if (FirewallCheck)
                firewallCheck = CheckFirewallRules(address.ToString()) ? SecurityState.Granted : SecurityState.Denied;
                //else
                //firewallCheck = SecurityState.Disabled;
            }
            catch (Exception ex)
            {
                //log exception
                firewallCheck = SecurityState.Denied;
            }

            // Check Key
            try
            {
                //if (AccessKeyCheck)
                accessKeyCheck = CheckAccessKeys(key) ? SecurityState.Granted : SecurityState.Denied;
                //else
                //accessKeyCheck = SecurityState.Granted;
            }
            catch (Exception ex)
            {
                //log exception
                accessKeyCheck = SecurityState.Denied;
            }

            securityCheck = firewallCheck != SecurityState.Denied && accessKeyCheck != SecurityState.Denied;

            return securityCheck;
        }

        /// <summary>
        /// Check if the IP Address is whitelisted.
        /// </summary>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        private static bool CheckFirewallRules(string ipAddress)
        {
            // check if dictionary is null
            // if null SetFirewallRules() -- return false

            // check if ipaddress exist

            bool firewallCheck = true;

            return firewallCheck;
        }

        /// <summary>
        /// Check if the Key is whitelisted.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static bool CheckAccessKeys(string key)
        {
            // check if dictionary is null
            // if null SetFirewallRules() -- return false

            // check if ipaddress exist

            bool keyCheck = true;

            return keyCheck;
        }
    }
}
