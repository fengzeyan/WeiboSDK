// /************************************************************************
//  * 项目名称 :  新浪微博Binding
//  * 项目描述 :     
//  * 版 本 号 :  v1.0.0.0 
//  * 说    明 :     
//  * 作    者 :  fzy
// ************************************************************************
//  * Copyright @ 上海动量惠银信息技术有限公司 2014. All rights reserved.
// ************************************************************************/
using System;
using System.Drawing;

using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WeiboAPI
{
    [BaseType(typeof(NSObject))]
    interface WeiboSDK
    {
        [Static,Export("isWeiboAppInstalled")]
        bool IsWeiboAppInstalled();

        [Static,Export("isCanShareInWeiboAPP")]
        bool IsCanShareInWeiboAPP();

        [Static,Export("isCanSSOInWeiboApp")]
        bool IsCanSSOInWeiboApp();

        [Static,Export("openWeiboApp")]
        bool OpenWeiboApp();

        [Static,Export("getWeiboAppInstallUrl")]
        string GetWeiboAppInstallUrl();

        [Static,Export("getWeiboAppSupportMaxSDKVersion")]
        string GetWeiboAppSupportMaxSDKVersion();

        [Static,Export("getSDKVersion")]
        string GetSDKVersion();

        [Static,Export("registerApp:")]
        bool RegisterApp(string appKey);

        [Static,Export("handleOpenURL:delegate:")]
        bool HandleOpenURL(NSUrl url, WeiboSDKDelegate weiboDelegate);

        [Static,Export("sendRequest:")]
        bool SendRequest(WBBaseRequest request);

        [Static,Export("sendResponse:")]
        bool SendResponse(WBBaseResponse response);

        [Static,Export("enableDebugMode:")]
        void EnableDebugMode(bool enabled);

        [Static,Export("logOutWithToken:delegate:withTag:")]
        void LogOutWithToken(string token, WBHttpRequestDelegate WBReqDelegate, string tag);

        [Static,Export("inviteFriend:withUid:withToken:delegate:withTag:")]
        void InviteFriend(string data, string withUid, string withToken, WBHttpRequestDelegate WBReqDelegate, string withTag);
    }

    [Model][Protocol]
    [BaseType(typeof(NSObject))]
    interface WeiboSDKDelegate
    {

        [Export("didReceiveWeiboRequest:")]
        void DidReceiveWeiboRequest(WBBaseRequest request);

        [Export("didReceiveWeiboResponse:")]
        void  DidReceiveWeiboResponse(WBBaseResponse response);
    }

    [Model][Protocol]
    [BaseType(typeof(NSObject))]
    public partial interface WBHttpRequestDelegate
    {

        [Export("request:didReceiveResponse:")]
        void DidReceiveResponse(WBHttpRequest request, NSUrlResponse response);

        [Export("request:didFailWithError:")]
        void DidFailWithError(WBHttpRequest request, NSError error);

        [Export("request:didFinishLoadingWithResult:")]
        void DidFinishLoadingWithResult(WBHttpRequest request, string result);

        [Export("request:didFinishLoadingWithDataResult:")]
        void DidFinishLoadingWithDataResult(WBHttpRequest request, NSData data);
    }


    [BaseType(typeof(NSObject))]
    public  interface WBHttpRequest
    {

        //      string url;
        //      string httpMethod;
        //      NSDictionary iparams;
        //      NSUrlConnection connection;
        //      NSMutableData responseData;
        //      WBHttpRequestDelegate idelegate;

        [Export("url", ArgumentSemantic.Retain)]
        string Url { get; set; }

        [Export("httpMethod", ArgumentSemantic.Retain)]
        string HttpMethod { get; set; }

        [Export("params", ArgumentSemantic.Retain)]
        NSDictionary Params { get; set; }

        [Export("connection", ArgumentSemantic.Retain)]
        NSUrlConnection Connection { get; set; }

        [Export("responseData", ArgumentSemantic.Retain)]
        NSMutableData ResponseData { get; set; }

        [Export("delegate", ArgumentSemantic.Assign)]
        WBHttpRequestDelegate Delegate { get; set; }

        [Export("tag", ArgumentSemantic.Retain)]
        string Tag { get; set; }

        [Static, Export("requestWithURL:httpMethod:params:delegate:withTag:")]
        void RequestWithURL(string url, string httpMethod, NSDictionary iparams, WBHttpRequestDelegate idelegate, string tag);

        [Static, Export("requestWithAccessToken:url:httpMethod:params:delegate:withTag:")]
        void RequestWithAccessToken(string accessToken, string url, string httpMethod, NSDictionary iparams, WBHttpRequestDelegate idelegate, string tag);

        [Export("disconnect")]
        void Disconnect();
    }

    [BaseType(typeof(NSObject))]
    interface WBDataTransferObject
    {
    
        [Export("userInfo")]
        NSDictionary UserInfo { get; set; }

   
        [Export("sdkVersion")]
        string SdkVersion { get; set; }

       
        [Export("shouldOpenWeiboAppInstallPageIfNotInstalled")]
        bool ShouldOpenWeiboAppInstallPageIfNotInstalled { get; set; }
    }

    // 微博SDK所有请求类的基类
    [BaseType(typeof(WBDataTransferObject))]
    interface WBBaseRequest
    {
        [Static,Export("request")]
        WBBaseRequest Request();
    }

    [BaseType(typeof(WBDataTransferObject))]
    interface WBBaseResponse
    {
       
        [Export("requestUserInfo")]
        NSDictionary RequestUserInfo { get; }

       
        [Export("statusCode")]
        WeiboSDKResponseStatusCode StatusCode { get; set; }

      
        [Static,Export("response")]
        WBBaseResponse Response();

    }


    [BaseType(typeof(WBBaseRequest))]
    interface WBAuthorizeRequest
    {
     
        [Export("redirectURI")]
        string RedirectURI { get; set; }

        [Export("shouldShowWebViewForAuthIfCannotSSO")]
        bool ShouldShowWebViewForAuthIfCannotSSO { get; set; }

        [Export("scope")]
        string Scope { get; set; }
    }

    [BaseType(typeof(WBBaseResponse))]
    interface WBAuthorizeResponse
    {
    
        [Export("userID")]
        string UserID { get; set; }

     
        [Export("accessToken")]
        string AccessToken { get; set; }

      
        [Export("expirationDate")]
        NSDate ExpirationDate { get; set; }
    }

    [BaseType(typeof(WBBaseRequest))]
    public  interface WBProvideMessageForWeiboRequest
    {
    }

    [BaseType(typeof(WBBaseResponse))]
    interface WBProvideMessageForWeiboResponse
    {

        [Export("message", ArgumentSemantic.Retain)]
        WBMessageObject Message { get; set; }

        [Static, Export("responseWithMessage:")]
        NSObject ResponseWithMessage(WBMessageObject message);


       
    }

    [BaseType(typeof(WBBaseRequest))]
    interface WBSendMessageToWeiboRequest
    {

        [Export("message", ArgumentSemantic.Retain)]
        WBMessageObject Message { get; set; }

        [Static, Export("requestWithMessage:")]
        NSObject RequestWithMessage(WBMessageObject message);

        [Static, Export("requestWithMessage:authInfo:access_token:")]
        NSObject RequestWithMessage(WBMessageObject message, WBAuthorizeRequest authRequest, string access_token);
    }


    [BaseType(typeof(WBBaseResponse))]
    interface WBSendMessageToWeiboResponse
    {

        [Export("authResponse")]
        WBAuthorizeResponse AuthResponse { get; set; }
    }

    [BaseType(typeof(NSObject))]
    interface WBMessageObject
    {
     
        [Export("text")]
        string Text { get; set; }


        [Export("imageObject")]
        WBImageObject ImageObject { get; set; }


        [Export("mediaObject")]
        WBBaseMediaObject MediaObject { get; set; }

      
        [Static,Export("message")]
        WBMessageObject Message { get; set; }
    }

    [BaseType(typeof(NSObject))]
    interface WBImageObject
    {
      
        [Export("imageData")]
        NSData ImageData { get; set; }

       
        [Export("object")]
        WBImageObject ImageObject();

       
        [Export("image")]
        UIImage Image();
    }



    [BaseType(typeof(NSObject))]
    interface WBBaseMediaObject
    {
      
        [Export("objectID")]
        string ObjectID { get; set; }

    
        [Export("title")]
        string Title { get; set; }

        [Export("description")]
        string Description { get; set; }

       
        [Export("thumbnailData")]
        string ThumbnailData { get; set; }

        [Export("scheme")]
        string Scheme { get; set; }

   
        [Export("object")]
        WBBaseMediaObject MediaObject { get; set; }
    }

    [BaseType(typeof(WBBaseMediaObject))]
    public  interface WBVideoObject
    {
        [Export("videoUrl", ArgumentSemantic.Retain)]
        string VideoUrl { get; set; }

        [Export("videoLowBandUrl", ArgumentSemantic.Retain)]
        string VideoLowBandUrl { get; set; }

        [Export("videoStreamUrl", ArgumentSemantic.Retain)]
        string VideoStreamUrl { get; set; }

        [Export("videoLowBandStreamUrl", ArgumentSemantic.Retain)]
        string VideoLowBandStreamUrl { get; set; }
    }

    [BaseType(typeof(WBBaseMediaObject))]
    public  interface WBMusicObject
    {

        [Export("musicUrl", ArgumentSemantic.Retain)]
        string MusicUrl { get; set; }

        [Export("musicLowBandUrl", ArgumentSemantic.Retain)]
        string MusicLowBandUrl { get; set; }

        [Export("musicStreamUrl", ArgumentSemantic.Retain)]
        string MusicStreamUrl { get; set; }

        [Export("musicLowBandStreamUrl", ArgumentSemantic.Retain)]
        string MusicLowBandStreamUrl { get; set; }
    }

    [BaseType(typeof(WBBaseMediaObject))]
    public  interface WBWebpageObject
    {

        [Export("webpageUrl", ArgumentSemantic.Retain)]
        string WebpageUrl { get; set; }
    }
}

