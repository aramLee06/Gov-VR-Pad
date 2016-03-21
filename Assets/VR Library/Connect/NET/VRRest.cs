using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;

using VR.Connect;

namespace VR.Connect.NET
{
	public class VRRest {
		public static string REST_METHOD_GET = "GET";
		public static string REST_METHOD_POST = "POST";
		public static string REST_METHOD_PUT = "PUT";
		public static string REST_METHOD_DELETE = "DELETE";

		public static int RESULT_ERROR = -1;
		public static int RESULT_FALSE = 0;
		public static int RESULT_TRUE = 1;
		public static int RESULT_CUSTOM_ERROR = 100;

		public static string Request(string endPoint) {
			return RequestURL(ConnectController.REST_URL, endPoint, "GET", "");
		} 

		public static string RequestURL(string baseURL, string endPoint, string method, string data) {
			string url = baseURL + endPoint;

			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

			request.Method = method;
			request.ContentLength = 0;
			request.ContentType = "application/json";

			if (!string.IsNullOrEmpty(data) && (method == REST_METHOD_POST || method == REST_METHOD_PUT)) {
				byte[] bytes = Encoding.UTF8.GetBytes (data);
				request.ContentLength = bytes.Length;
				request.Timeout = 10000;

				try {
					using (Stream writeStream = request.GetRequestStream()) {
						if (writeStream != null) {	
							writeStream.Write(bytes, 0, bytes.Length);
							writeStream.Close();
						}
					}
				} catch (Exception e) {
					return null;
				}
			}

			try {
				var response = (HttpWebResponse)request.GetResponse();
				var responseValue = string.Empty;
				if (response.StatusCode != HttpStatusCode.OK) {
					string message = "Request failed:" + response.StatusCode + "," + response.StatusDescription;
					return null;
				}

				using (var responseStream = response.GetResponseStream()) {
					if (responseStream != null)
						using (var reader = new StreamReader(responseStream)) {
							responseValue = reader.ReadToEnd();
						}
				}
				return responseValue;
			} catch (Exception e) {
			}

			return null;
		}
	}
}
