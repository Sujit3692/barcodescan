using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
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
	public string MovementType;
	public string StockType;
	public string SalesOrder;
	public string SalesOrderItem;
	public string IssuedQuantity;
	public string IssuedUnitofMeasure;
	public string EntryQuantity;
	public string EntryUnitOfIssue;
	public string TotalQuantity;
	public string PONumber;
	public string POItem;
	public bool DeliveryComplete;
	public string Vendor;
	public string ReceivingMaterialNumber;
	public string ReceivingPlant;
	public string ReceivingStorageLocation;
	public string ItemText;
	public string Asset;
	public string Recipient;
	public string CostCenter;
	public string OrderNumber;
	public string OrderActivityNumber;
	public string ReservationNo;
	public string ReservationItem;
	public string ReservationType;
	public string FinalReservationIssue;
	public string MovementIndicator;
	public string MovementReason;
	public string WBSElement;
	public string Network;
	public string NetworkActivityNumber;
	public string Activity;
	public string ReferenceDoc;
	public string ReferenceDocYear;
	public string ReferenceDocItem;
	public string GLAccount;
	public string StorageBin;
	public string GRNumber;
	public string DeliveryNo;
	public string DeliveryItem;
	public string IsOK;
	public string ValuationType;
	public string AppMode;
	public object NavSerial;
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


[RequireComponent(typeof(VoiceController))]
public class VoiceTest : MonoBehaviour {

    public Text uiText;
	public Text TextHeader;
	VoiceController voiceController;

    public void GetSpeech() {
		
		voiceController.GetSpeech();
    }

    void Start() {
        voiceController = GetComponent<VoiceController>();
    }

    void OnEnable() {
        VoiceController.resultRecieved += OnVoiceResult;
    }

    void OnDisable() {
        VoiceController.resultRecieved -= OnVoiceResult;
    }

    void OnVoiceResult(string text)
    {
		var matdocId = "";
		int quan;
		quan = Int16.Parse(text);
		//TextHeader.text = SimpleDemo.barcodevalue + " / " + SimpleDemo.desc;
		
		String username = "leusa";
		String password = "chonflas";
		//SimpleDemo demo = new SimpleDemo();
		String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(username + ":" + password));
		try { 
		var client = new RestClient("https://convergentis.prod.apimanagement.us10.hana.ondemand.com:443/GOODS_MOVEMENT_SRV/MaterialDocumentHeaderSet");
		//client.Timeout = -1;
		var request1 = new RestRequest(Method.POST);
		request1.AddHeader("ApiKey", "fkIpAYGzYjsGOsSgrpETyMsB2713EleR");
		request1.AddHeader("Content-Type", "application/json");
		request1.AddHeader("X-CSRF-TOKEN", SimpleDemo.token);
		request1.AddHeader("Authorization", "Basic " + encoded);
		request1.AddCookie(SimpleDemo.CookieName1, SimpleDemo.CookieValue1);
		request1.AddCookie(SimpleDemo.CookieName2, SimpleDemo.CookieValue2);
			request1.AddParameter
		("application/json", "{\r\n\t\"PostingDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\"DocumentDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\"RefDocumentNo\":\"\"," +
		"\r\n\t\"BillOfLading\":\"\",\r\n\t\"GRGISlipNo\":\"\",\r\n\"HeaderText\":\"\",\r\n\"GoodsMovementRefDocType\":\"R10\",\r\n\"Tcode\":\"MIGO_TP\",\r\n\"Attachments\":[]," +
		"\r\n\"MaterialDocumentItemsSet\":\r\n\t[\r\n\t\t{\r\n\t\t\t\"Material\":\"" + SimpleDemo.barcodevalue +
		"\",\r\n\t\t\t\"ItemText\":\"" + SimpleDemo.desc +
		"\",\r\n\t\t\t\"EntryQuantity\":\"" + quan +
		"\",\r\n\t\t\t\"EntryUnitOfIssue\":\"KG\"," +
		"\r\n\t\t\t\"Recipient\":\"\",\r\n\t\t\t\"Plant\":\"3000\",\r\n\t\t\t\"ValuationType\":\"\"," +
		"\r\n\t\t\t\"StorageLocation\":\"0001\"," +
		"\r\n\t\t\t\"MovementType\":\"311\",\r\n\t\t\t\"NavSerial\":[],\r\n\t\t\t\"Batch\":\"\"," +
		"\r\n\t\t\t\"ReceivingStorageLocation\":\"0002\"\r\n\t\t\t\t\t}]}", ParameterType.RequestBody);
		//request1.AddParameter("application/json", "{\r\n\t\t\t\"PostingDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\t\t\t\t\t\"DocumentDate\":\"\\/Date(1602489213688)\\/\",\r\n\t\t\t\t\t\t\"RefDocumentNo\":\"\",\r\n\"BillOfLading\":\"\",\r\n\"GRGISlipNo\":\"\",\r\n\"HeaderText\":\"\",\r\n\"GoodsMovementRefDocType\":\"R10\",\r\n\"Tcode\":\"MIGO_TP\",\r\n\"Attachments\":[],\r\n\"MaterialDocumentItemsSet\":\r\n\t[\r\n\t\t{\r\n\t\t\t\"Material\":\"GTS-001\",\r\n\t\t\t\"ItemText\":\"AMMONIUM NITRATE\",\r\n\t\t\t\"EntryQuantity\":\"1\",\r\n\t\t\t\"EntryUnitOfIssue\":\"G\",\r\n\t\t\t\"Recipient\":\"\",\r\n\t\t\t\"Plant\":\"3000\",\r\n\t\t\t\"ValuationType\":\"\",\r\n\t\t\t\"StorageLocation\":\"0001\",\r\n\t\t\t\"MovementType\":\"311\",\r\n\t\t\t\"NavSerial\":[],\r\n\t\t\t\"Batch\":\"\",\r\n\t\t\t\"ReceivingStorageLocation\":\"0002\"\r\n\t\t\t\t\t}]}", ParameterType.RequestBody);
		IRestResponse response1 = client.Execute(request1);
		string presponse = response1.Content;
		PostRoot resdata = JsonUtility.FromJson<PostRoot>(presponse);
		var postdetails = JsonConvert.DeserializeObject<PostRoot>(presponse);

		matdocId = postdetails.d.MaterialDocumentID;

		}
			catch (Exception e)
			{
				Console.Out.WriteLine("-----------------");
				Console.Out.WriteLine(e.Message);
			}

		uiText.text = "Material Document " + matdocId + " created successfully";

	}
}
