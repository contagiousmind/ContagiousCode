using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Threading.Tasks;

namespace ContagiousCode.Web
{
    public class Handler
    {

        private HttpContext c_context;

        public HttpContext Context
        {
            get
            {
                return c_context;
            }
            set
            {
                c_context = value;
            }
        }

        public Dictionary<string, string> QueryFormValues()
        {
            return c_queryFormValues;
        }

        private Dictionary<string, string> c_queryFormValues = null;

        private string c_method = String.Empty;

        private Ajax c_ajax = new Ajax();

        public Ajax Ajax
        {
            get
            {
                return c_ajax;
            }
            set
            {
                c_ajax = value;
            }
        }

        public string Method
        {
            get
            {
                return c_method;
            }
            set
            {
                c_method = value;
            }
        }

        /// <summary>
        /// Bases the setup.
        /// </summary>
        /// <param name="context">The context.</param>
        public void Setup(HttpContext context)
        {
            Context = context;
            SetupQueryValues();
            SetBaseValues();
            c_method = QueryFormValues()["Method"];
        }



        /// <summary>
        /// Returns the data.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="data">The data.</param>
        /// <param name="encrypt">if set to <c>true</c> [encrypt].</param>
        public void ReturnData()
        {
            string temp = JsonConvert.SerializeObject(c_ajax, Formatting.Indented);
            Context.Response.Write(temp);
        }


        private void SetBaseValues()
        {
            try
            {
                c_queryFormValues = QueryStringToDictionary();
            }
            catch (Exception ex) { }

            if (c_queryFormValues == null)
            {
                c_queryFormValues = new Dictionary<string, string>();
            }
        }

        private void SetupQueryValues()
        {
            //c_form = "";
            //c_click = "";
            //c_action = "";
            //c_chart = "";

            //c_form = QueryStringManager.GetQueryStringValue("Form", "");
            //c_click = QueryStringManager.GetQueryStringValue("Click", "");
            //c_action = QueryStringManager.GetQueryStringValue("Action", "");
            //c_chart = QueryStringManager.GetQueryStringValue("Chart", "");

            //c_queryValues = QueryStringManager.QueryValues();
            //c_formValues = QueryStringManager.FormValues();
        }


        public string GetQueryFormValue(string keyName)
        {
            return c_queryFormValues[keyName];
        }

        public T GetQueryFormValue<T>(string keyName, T defaultValue)
        {
            return GetQueryFormValue<T>(keyName, defaultValue, "dd/MM/yyyy HH:mm:ss");
        }

        public T GetQueryFormValue<T>(string keyName, T defaultValue, string dateFormat)
        {
            T result = default(T);

            if (c_queryFormValues == null || c_queryFormValues.Count == 0)
            {
                return result;
            }

            if (!c_queryFormValues.ContainsKey(keyName))
            {
                return result;
            }

            string value = c_queryFormValues[keyName];

            if (typeof(T) == typeof(DateTime))
            {
                try
                {
                    result = (T)Convert.ChangeType(ContagiousCode.Util.Util.StringToDate(value, dateFormat), TypeCode.Object);
                }
                catch (Exception ex)
                {
                    result = defaultValue;
                }
            }
            else
            {
                Type ot = typeof(T);

                try
                {
                    result = (T)Convert.ChangeType(value, typeof(T));
                }
                catch (Exception ex)
                {
                    result = defaultValue;
                }
            }

            return result;
        }

        private static Dictionary<String, String> QueryStringToDictionary()
        {
            HttpContext current = HttpContext.Current;
            Dictionary<String, String> result = new Dictionary<string, string>();
            foreach (string key in current.Request.Form.Keys)
            {
                result.Add(key, current.Request.Form.GetValues(key)[0]);
            }

            foreach (string key in current.Request.QueryString.Keys)
            {
                result.Add(key, current.Request.QueryString.GetValues(key)[0]);
            }


            //string queryString = "";
            //queryString = HttpUtility.UrlDecode(queryString);
            //string[] temp1 = queryString.Split('&');

            //for (int i = 0; i < temp1.Length; i++)
            //{
            //    string[] temp2 = temp1[i].Split('=');
            //    if (temp2.Length == 2)
            //    {
            //        result.Add(temp2[0], temp2[1]);
            //    }
            //}

            return result;

        }



    }
}
