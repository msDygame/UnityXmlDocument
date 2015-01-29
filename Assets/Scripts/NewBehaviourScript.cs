using UnityEngine;
using System.Collections;
using System.Xml;//for XmlDocument 類別

public class NewBehaviourScript : MonoBehaviour
{
	void Awake()
	{
	//SAMPLE:
/*
	<manifest 
		android:versionCode="1" 
		android:versionName="1.0" 
		android:installLocation="internalOnly" 
		package="com.dygame.gamezone2"
  		xmlns:android="http://schemas.android.com/apk/res/android">

		<application android:label="@string/app_name" android:icon="@drawable/app_icon" android:debuggable="false">
			<xperson xname="gengzun" xsex="mon" xage="24"/>
			<person name="fengyun" sex="man" age="23">
				<pass>hunton</pass>
			</person>
			<meta-data android:value="028" android:name="CHANNEL"/>
			<activity 
				android:label="@string/app_name" 
				android:name="com.dygame.gamezone2.PreLoader" 
				android:screenOrientation="landscape" 
				android:theme="@android:style/Theme.NoTitleBar.Fullscreen" 
				android:configChanges="keyboard|keyboardHidden|orientation" 
				android:launchMode="singleInstance" 
				android:hasCode="true">
				<intent-filter>   
				
				</intent-filter>   
			</activity>
	 	</application>	
*/	 	
	}
	// Use this for initialization
	void Start () 
	{
	
	}	
	// Update is called once per frame
	void Update () 
	{
	
	}
	//
	void OnGUI()
	{	
		if (GUI.Button(new Rect(10, 10 , 300, 30), "open XmlDocument"))
		{
			//載入指定xml文件
			string sTargetFile = @".\Assets\Plugins\Android\AndroidManifest.xml" ;
			XmlDocument doc = new XmlDocument();
			doc.Load(sTargetFile);

			//選擇節點
		  //XmlNodeList list = doc.DocumentElement.GetElementsByTagName("TAG"); 
			
			//指定一個節點
			XmlNode node = doc.SelectSingleNode("/manifest/application/meta-data[@android:name='CHANNEL']");//不行,node為null
		//	string sn1 = string.Copy(node.OuterXml) ;//不行
			XmlElement theBook = (XmlElement)doc.SelectSingleNode("/manifest/application/meta-data[@android:name='CHANNEL']");//不行,theBook為null
		//	string sn2 = string.Copy(theBook.OuterXml) ;//不行

			XmlNode dNode1 = doc.SelectSingleNode("/manifest");
			XmlNode dNode2 = doc.SelectSingleNode("/manifest/application");
			XmlNode dNode3 = doc.SelectSingleNode("/manifest/application/meta-data");
			XmlNode dNode4 = doc.SelectSingleNode("/manifest/application/person");
			XmlNode dNode5 = doc.SelectSingleNode("/manifest/application/xperson");

			XmlElement childElement2 = (XmlElement)dNode2;
			XmlElement childElement3 = (XmlElement)dNode3;
		//	XmlElement childElement4 = (XmlElement)dNode4;//不行,因為還有子節點?
			XmlElement childElement5 = (XmlElement)dNode5;
			XmlElement xSlected = (XmlElement)doc.SelectSingleNode("/manifest/application/meta-data") ;

			//獲取指定節點中的文本
			string id1 = dNode1.Attributes["package"].Value;//OK! get "com.dygame.gamezone2"
			string id5 = dNode3.OuterXml ;//OK! get "<meta-data android:value="028" android:name="CHANNEL"/>" xmlns:android="http://schemas.android.com/apk/res/android"
			string id4 = dNode3.InnerXml ;//empty

			//獲取指定節點的指定屬性值
		//	string id6 = dNode3.Attributes["android:value"].Value ;
			string id2 = childElement2.Attributes["android:debuggable"].Value;//OK! get "false"
			string id3 = childElement3.Attributes["android:value"].Value;//OK! get "028"
			string content3=childElement3.InnerText;//empty
			string content2=childElement2.InnerText;//OK! get "hunton"
			//temp
			string sValue = "" ;
			string sTeste = "" ;
			if (childElement5.GetAttribute("xname") == "gengzun")
	//		if (childElement3.GetAttribute("name") == "CHANNEL")
//			if (childElement3.GetAttribute("android:name") == "CHANNEL")   
			{   
				sValue = childElement5.GetAttribute("xsex") ;//OK! get "mom"
				sTeste = childElement3.GetAttribute("android:value") ;//OK! get "028"
			}

			XmlNode dNode6 = doc.SelectSingleNode("/manifest/application/meta-data[@android:value='CHANNEL']");//不行,node為null 
		//	string sn6 = dNode6.OuterXml ;
			XmlNode dNode7 = doc.SelectSingleNode("/manifest/application/meta-data[@value='CHANNEL']");//不行,node為null  
		//	string sn7 = dNode7.OuterXml ;

			//生成一個新節點
			XmlElement xEle1 = doc.CreateElement("uses-permission");
			//為指定節點的新建屬性 命名空間無效?
			XmlAttribute xAtt1 = doc.CreateAttribute("android", "name", dNode1.NamespaceURI);
			xAtt1.Value = "android.permission.READ_PHONE_STATE";//Fail! set "<uses-permission name="android.permission.READ_PHONE_STATE" />"
			xEle1.Attributes.Append(xAtt1);
			//命名空間有效!
			string ns = "http://schemas.android.com/apk/res/android" ;//namespace
			XmlAttribute xAtt2 = doc.CreateAttribute("android","ForTheIronHorde", ns);
			xAtt2.Value = "android.permission.INTERNET" ;//OK! set "<uses-permission android:ForTheIronHorde="android.permission.INTERNET" />"
			XmlElement xEle2 = doc.CreateElement("uses-permission");
			xEle2.Attributes.Append(xAtt2);
			//為指定節點的新建屬性並賦值
			childElement3.SetAttribute("android:value","29");
			//為指定節點添加子節點
			dNode1.AppendChild(xEle1);
			dNode1.AppendChild(xEle2);
			//保存XML文件
			doc.Save(sTargetFile);
		}
	}
}
