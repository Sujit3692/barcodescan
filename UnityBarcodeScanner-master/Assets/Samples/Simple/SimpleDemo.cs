using BarcodeScanner;
using BarcodeScanner.Scanner;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Wizcorp.Utils.Logger;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json.Serialization;
using RestSharp;


[System.Serializable]
public class Metadata
{
	public string id;
	public string uri;
	public string type;

}

[System.Serializable]
public class Result
{
	public Metadata __metadata;
	public string Tcode;
	public string GoodsMovementRefDocType;
	public string Item;
	public string Plant;
	public string StorageLocation;
	public string MovementType;
	public string DefaultUnitOfMeasure;
	public bool isSerialized;
	public bool isBatchManaged;
	public string Description;
}

[System.Serializable]
public class D
{
	//public Result result;
	public Result[] results;
}

[System.Serializable]
public class Root
{
	//public Metadata meta;
	//public Result result;
	public D d;
}


/*[System.Serializable]
public class PostResult
{
	public Metadata __metadata;
	public string GoodsMovementRefDocType;
	public string ObjectKey;
	public string LineNumber;
	public string Tcode;
	public string Material;
	public string Plant;
	public string StorageLocation;
	public string Batch;
	public string MovementType ;
	public string StockType ;
	public string SalesOrder ;
	public string SalesOrderItem ;
	public string IssuedQuantity ;
	public string IssuedUnitofMeasure ;
	public string EntryQuantity ;
	public string EntryUnitOfIssue ;
	public string TotalQuantity ;
	public string PONumber ;
	public string POItem ;
	public bool DeliveryComplete ;
	public string Vendor ;
	public string ReceivingMaterialNumber ;
	public string ReceivingPlant ;
	public string ReceivingStorageLocation ;
	public string ItemText ;
	public string Asset ;
	public string Recipient ;
	public string CostCenter ;
	public string OrderNumber ;
	public string OrderActivityNumber ;
	public string ReservationNo ;
	public string ReservationItem ;
	public string ReservationType ;
	public string FinalReservationIssue ;
	public string MovementIndicator ;
	public string MovementReason ;
	public string WBSElement ;
	public string Network ;
	public string NetworkActivityNumber ;
	public string Activity ;
	public string ReferenceDoc ;
	public string ReferenceDocYear ;
	public string ReferenceDocItem ;
	public string GLAccount ;
	public string StorageBin ;
	public string GRNumber ;
	public string DeliveryNo ;
	public string DeliveryItem ;
	public string IsOK ;
	public string ValuationType ;
	public string AppMode ;
	public object NavSerial ;
}

[System.Serializable]
public class MaterialDocumentItemsSet
{
	public PostResult[] results;
}

[System.Serializable]
public class Dt
{
	public Metadata __metadata;
	public string Tcode;
	public string GoodsMovementRefDocType;
	public string MaterialDocumentID;
	public DateTime PostingDate;
	public DateTime DocumentDate;
	public string RefDocumentNo;
	public string BillOfLading;
	public string GRGISlipNo;
	public string HeaderText;
	public MaterialDocumentItemsSet MaterialDocumentItemsSet;
	public object Attachments;
}

[System.Serializable]
public class PostRoot
{
	public Dt d;
}
*/

public class SimpleDemo : MonoBehaviour
{

	private IScanner BarcodeScanner;
	public Text TextHeader;
	public RawImage Image;
	public AudioSource Audio;
	public static String token;
	public static String CookieName1;
	public static String CookieName2;
	public static String CookieValue1;
	public static String CookieValue2;
	public static String barcodevalue;
	public static String desc;

	/*[SerializeField]
	private Text m_Hypotheses;

	[SerializeField]
	private Text m_Recognitions;

	private DictationRecognizer m_DictationRecognizer;*/

	// Disable Screen Rotation on that screen
	void Awake()
	{
		Screen.autorotateToPortrait = false;
		Screen.autorotateToPortraitUpsideDown = false;
	}

	void Start()
	{
		// Create a basic scanner
		BarcodeScanner = new Scanner();
		BarcodeScanner.Camera.Play();

		// Display the camera texture through a RawImage
		BarcodeScanner.OnReady += (sender, arg) => {
			// Set Orientation & Texture
			Image.transform.localEulerAngles = BarcodeScanner.Camera.GetEulerAngles();
			Image.transform.localScale = BarcodeScanner.Camera.GetScale();
			Image.texture = BarcodeScanner.Camera.Texture;

			// Keep Image Aspect Ratio
			var rect = Image.GetComponent<RectTransform>();
			var newHeight = rect.sizeDelta.x * BarcodeScanner.Camera.Height / BarcodeScanner.Camera.Width;
			rect.sizeDelta = new Vector2(rect.sizeDelta.x, newHeight);
		};

		// Track status of the scanner
		BarcodeScanner.StatusChanged += (sender, arg) => {
			TextHeader.text = "Status: " + BarcodeScanner.Status;
		};
	}

	/// <summary>
	/// The Update method from unity need to be propagated to the scanner
	/// </summary>
	void Update()
	{
		if (BarcodeScanner == null)
		{
			return;
		}
		BarcodeScanner.Update();
	}

	#region UI Buttons

	public void ClickStart()
	{
		if (BarcodeScanner == null)
		{
			Log.Warning("No valid camera - Click Start");
			return;
		}

		// Start Scanning
		BarcodeScanner.Scan((barCodeType, barCodeValue) =>
		{
			BarcodeScanner.Stop();
			string URL = "https://convergentis.prod.apimanagement.us10.hana.ondemand.com:443/GOODS_MOVEMENT_SRV/ItemsSet?$filter=Plant eq '3000' and Description eq '" + barCodeValue + "'&$format=json";
			CookieContainer container = new CookieContainer();
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
			request.Method = "GET";
			request.ContentType = "application/json";
			request.Headers.Add("ApiKey", "fkIpAYGzYjsGOsSgrpETyMsB2713EleR");
			request.CookieContainer = container;

			String username = "leusa";
			String password = "chonflas";
			String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
			request.Headers.Add("Authorization", "Basic " + encoded);
			request.Headers["x-csrf-token"] = "Fetch";

			try
			{
				HttpWebResponse webResponse = (HttpWebResponse)request.GetResponse();
				Stream webStream = webResponse.GetResponseStream();
				StreamReader responseReader = new StreamReader(webStream);
				string response = responseReader.ReadToEnd();
				Root user = JsonUtility.FromJson<Root>(response);
				var details = JsonConvert.DeserializeObject<Root>(response);
				token = webResponse.Headers.Get("X-CSRF-TOKEN");

				barcodevalue = barCodeValue;
				desc = details.d.results[0].Description;
				TextHeader.text = barCodeValue + " / " + details.d.results[0].Description + " / " + details.d.results[0].DefaultUnitOfMeasure + " / " + details.d.results[0].Plant;
				responseReader.Close();

				CookieName1 = webResponse.Cookies[0].Name;
				CookieName2 = webResponse.Cookies[1].Name;
				CookieValue1 = webResponse.Cookies[0].Value;
				CookieValue2 = webResponse.Cookies[1].Value;
				//BarcodeScanner.Camera.Stop();
				SceneManager.LoadScene("Input");


				/*var client = new RestClient("https://convergentis.prod.apimanagement.us10.hana.ondemand.com:443/GOODS_MOVEMENT_SRV/MaterialDocumentHeaderSet");
				//client.Timeout = -1;
				var request1 = new RestRequest(Method.POST);
				request1.AddHeader("ApiKey", "fkIpAYGzYjsGOsSgrpETyMsB2713EleR");
				request1.AddHeader("Content-Type", "application/json");
				request1.AddHeader("X-CSRF-TOKEN", webResponse.Headers.Get("X-CSRF-TOKEN"));
				request1.AddHeader("Authorization", "Basic " + encoded);
				request1.AddCookie(webResponse.Cookies[0].Name, webResponse.Cookies[0].Value);
				request1.AddCookie(webResponse.Cookies[1].Name, webResponse.Cookies[1].Value);
				request1.AddParameter
				("application/json", "{\r\n\t\"PostingDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\"DocumentDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\"RefDocumentNo\":\"\"," +
				"\r\n\t\"BillOfLading\":\"\",\r\n\t\"GRGISlipNo\":\"\",\r\n\"HeaderText\":\"\",\r\n\"GoodsMovementRefDocType\":\"R10\",\r\n\"Tcode\":\"MIGO_TP\",\r\n\"Attachments\":[]," +
				"\r\n\"MaterialDocumentItemsSet\":\r\n\t[\r\n\t\t{\r\n\t\t\t\"Material\":\"" + barCodeValue +
				"\",\r\n\t\t\t\"ItemText\":\"" + details.d.results[0].Description +
				"\",\r\n\t\t\t\"EntryQuantity\":\"1\"," +
				"\r\n\t\t\t\"EntryUnitOfIssue\":\"KG\"," +
				"\r\n\t\t\t\"Recipient\":\"\",\r\n\t\t\t\"Plant\":\"3000\",\r\n\t\t\t\"ValuationType\":\"\"," +
				"\r\n\t\t\t\"StorageLocation\":\"0001\"," +
				"\r\n\t\t\t\"MovementType\":\"311\",\r\n\t\t\t\"NavSerial\":[],\r\n\t\t\t\"Batch\":\"\"," +
				"\r\n\t\t\t\"ReceivingStorageLocation\":\"0002\"\r\n\t\t\t\t\t}]}", ParameterType.RequestBody);
				//request1.AddParameter("application/json", "{\r\n\t\t\t\"PostingDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\t\t\t\t\t\"DocumentDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\t\t\t\t\t\"RefDocumentNo\":\"\",\r\n\"BillOfLading\":\"\",\r\n\"GRGISlipNo\":\"\",\r\n\"HeaderText\":\"\",\r\n\"GoodsMovementRefDocType\":\"R10\",\r\n\"Tcode\":\"MIGO_TP\",\r\n\"Attachments\":[],\r\n\"MaterialDocumentItemsSet\":\r\n\t[\r\n\t\t{\r\n\t\t\t\"Material\":\"GTS-001\",\r\n\t\t\t\"ItemText\":\"AMMONIUM NITRATE\",\r\n\t\t\t\"EntryQuantity\":\"1\",\r\n\t\t\t\"EntryUnitOfIssue\":\"G\",\r\n\t\t\t\"Recipient\":\"\",\r\n\t\t\t\"Plant\":\"3000\",\r\n\t\t\t\"ValuationType\":\"\",\r\n\t\t\t\"StorageLocation\":\"0001\",\r\n\t\t\t\"MovementType\":\"311\",\r\n\t\t\t\"NavSerial\":[],\r\n\t\t\t\"Batch\":\"\",\r\n\t\t\t\"ReceivingStorageLocation\":\"0002\"\r\n\t\t\t\t\t}]}", ParameterType.RequestBody);
				IRestResponse response1 = client.Execute(request1);
				string presponse = response1.Content;
				PostRoot resdata = JsonUtility.FromJson<PostRoot>(presponse);
				var postdetails = JsonConvert.DeserializeObject<PostRoot>(presponse);

				var matdocId = postdetails.d.MaterialDocumentID;*/

			}
			catch (Exception e)
			{
				Console.Out.WriteLine("-----------------");
				Console.Out.WriteLine(e.Message);
			}

			
			Audio.Play();
			//#if UNITY_ANDROID || UNITY_IOS
			//			Handheld.Vibrate();
			//#endif


		});


	}

	public void ClickStop()
	{
		if (BarcodeScanner == null)
		{
			Log.Warning("No valid camera - Click Stop");
			return;
		}

		// Stop Scanning
		BarcodeScanner.Stop();
	}

	public void ClickBack()
	{
		// Try to stop the camera before loading another scene
		StartCoroutine(StopCamera(() => {
			SceneManager.LoadScene("Boot");
		}));
	}

	/// <summary>
	/// This coroutine is used because of a bug with unity (http://forum.unity3d.com/threads/closing-scene-with-active-webcamtexture-crashes-on-android-solved.363566/)
	/// Trying to stop the camera in OnDestroy provoke random crash on Android
	/// </summary>
	/// <param name="callback"></param>
	/// <returns></returns>
	public IEnumerator StopCamera(Action callback)
	{
		// Stop Scanning
		Image = null;
		BarcodeScanner.Destroy();
		BarcodeScanner = null;

		// Wait a bit
		yield return new WaitForSeconds(0.1f);

		callback.Invoke();
	}

	#endregion
}


// response = JsonUtility.FromJson<>(response);
//				// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
