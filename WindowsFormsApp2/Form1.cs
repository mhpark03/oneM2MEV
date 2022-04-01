using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using System.Xml.Linq;
using ExcelLibrary.SpreadSheet;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Threading;
using System.Web;
using System.Net.Sockets;

/*
* 8000번 포트 리스닝을 위한 선행 작업 
* netsh http add urlacl url=http://*:8000/ user=everyone
*/

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        private Thread rTh;

        private enum states
        {
            closed,
            idle,
            getmodel,
            getmanufac,
            getimsi,
            getimei,
            geticcid,
            autogetmodel,
            autogetmodelgmm,
            autogetmanufac,
            autogetimsi,
            autogetimei,
            autogeticcid,
            bootstrap,
            setserverinfo,
            setserverinfotpb23,
            setmefserverinfo,
            sethttpserverinfo,
            setfotaserverinfo,
            getserverinfo,
            setncdp,
            setservertype,
            setepns,
            setmbsps,
            setAutoBS,
            register,
            deregister,
            sendLWM2Mdata,
            receiveLWM2Mdata,
            downloadMDLFOTA,
            updateMDLFOTA,
            lwm2mreset,
            sendmsgstr,
            sendmsghex,
            sendmsgver,
            getdevip,

            seteventalt1,
            seteventalt2,
            seteventalt3,
            seteventalt4,

            disable_bg96,
            enable_bg96,
            setcdp_bg96,

            holdoffbc95,
            lwm2mresetbc95,
            getsvripbc95,
            setsvrbsbc95,
            setsvripbc95,
            actsetsvrbsbc95,
            actsetsvripbc95,
            autosetsvrbsbc95,
            autosetsvripbc95,
            getepnsbc95,
            setepnsbc95,
            autosetepnsbc95,
            getmbspsbc95,
            setmbspsbc95,
            autosetmbspsbc95,
            bootstrapbc95,
            rebootbc95,

            sendonemsgstr,
            sendonemsgstrchk,
            sendonemsgsvr,
            responemsgsvr,
            sendonedevstr,
            sendonedevdb,
            sendonedevdb2,

            getonem2mmode,
            setonem2mmode,
            setmefauthnt,
            setmefauth,
            fotamefauthnt,
            mfotamefauth,
            getCSEbase,
            getremoteCSE,
            setremoteCSE,
            updateremoteCSE,
            delremoteCSE,
            setcontainer,
            settxcontainer,
            delcontainer,
            delcontainer2,
            setsubscript,
            delsubscript,
            getonem2mdata,
            getACP,
            setACP,
            updateACP,
            delACP,
            setrcvauto,
            setrcvmanu,
            resetreceived,
            resetboot,
            resetmodechk,
            resetmodechked,
            resetmodeset,
            resetmodeseted,
            resetmefauth,
            resetreport,

            setcereg,
            setceregtpb23,
            getcereg,
            reset,

            getimeitpb23,
            geticcidtpb23,
            autogetimeitpb23,
            autogeticcidtpb23,
            resettpb23,
            bootstrapmodetpb23,
            setepnstpb23,
            setmbspstpb23,
            bootstraptpb23,
            registertpb23,
            deregistertpb23,
            registerbc95,
            deregisterbc95,
            sendLWM2Mdatatpb23,
            receiveLWM2Mdatatpb23,
            downloadMDLFOTAtpb23,
            updateMDLFOTAtpb23,
            lwm2mresettpb23,
            sendmsgstrtpb23,
            sendmsgstrbc95,
            sendmsghextpb23,
            sendmsgvertpb23,
            sendmsgverbc95,

            setmbspmodel,
            setmbspsn,
            setmbspcfg,

            geticcidamtel,
            autogeticcidamtel,

            geticcidme,
            autogeticcidme,
            setepnsme,
            sendmsgverme,

            geticcidlg,
            autogeticcidlg,

            geticcidbc95,
            autogeticcidbc95,

            getmodemSvrVer,
            modemFWUPreport,
            modemFWUPfinishLTE,
            modemFWUPstart,
            modemFWUPfinish,
            modemFWUPboot,
            modemFWUPmodechk,
            modemFWUPmodechked,
            modemFWUPmodeset,

            getdeviceSvrVer,
            setdeviceSvrVer,
            deviceFWUPfinish,
            deviceFWUPstart,
            deviceFWDownload,
            deviceFWDownloading,
            deviceFWDLfinsh,

            deviceFWList,
            deviceFWOpen,
            deviceFWRead,
            deviceFWClose,

            catm1check,
            catm1set,
            catm1apn1,
            catm1apn2,
            catm1psmode,
            rfoff,
            rfon,
            rfreset,

            catm1imscheck,
            catm1imsset,
            catm1imsapn1,
            catm1imsapn2,
            catm1imsmode,
            catm1imspco,

            nbcheck,
            nbset,
            nbapn1,
            nbapn2,
            nbpsmode,

            getmodemver,
            autogetmodemver,
            getmodemvernt,
            autogetmodemvernt,
            getmodemverbc95,
            autogetmodemverbc95,
            getNWmode,
            autogetNWmode,

            testatcmd,

            onem2mtc0201011,        // MEF server 설정
            onem2mtc0201012,        // BRK server 설정
            onem2mtc0201013,        // FOTA server 설정
            onem2mtc0201014,        // server 설정 값 확인
            onem2mtc0201021,        // 플랫폼 Agent 동작 확인
            onem2mtc0201022,        // 플랫폼 Agent  동작 설정
            onem2mtc0201023,        // 플랫폼 Agent 설정 결과 확인
            onem2mtc0201024,        // 플랫폼 Agent 설정 완료

            onem2mtc0202011,         // MEF 인증 요청
            onem2mtc0202012,         // MEF 인증 요청

            onem2mtc020301,         // remoteCSE 조회 결과에 따라 remoteCSE 신규생성/업데이트 분기 

            onem2mtc020401,         // CSEBase  조회

            onem2mtc0205011,        // remoteCSE 조회-업데이트
            onem2mtc0205012,        // remoteCSE 신규 생성
            onem2mtc0205021,        // 수신 폴더 생성
            onem2mtc0205022,        // 송신 폴더 생성 - 구독 설정 시험 진행
            onem2mtc0205031,        // 구독 1회 신청 - 성공시 데이터 전송/오류시 구독 삭제 요청
            onem2mtc0205032,        // 구독 재 생성 - 데이터 전송
            onem2mtc0205041,         // 서버 동작시 송신 폴더에 데이터 생성
            onem2mtc0205042,
            onem2mtc0205043,         // 단말 단독 실행시 수신 폴더에 데이터 생성
            onem2mtc020505,        // remote 업데이트 - 데이터 송신

            onem2mtc0206011,         // data noti 이벤트 (서버에서 수신)
            onem2mtc0206012,         // data noti 이벤트 (단말 self)
            onem2mtc020602,         // data 수신 모드 설정 (자동)
            onem2mtc0206031,         // 자동수신 (서버에서 수신)
            onem2mtc0206032,         // 자동수신 (단말 self)
            onem2mtc0206041,         // 구독신청 - data 수신
            onem2mtc0206042,         // 자동수신 - 수동설정 - data 수신

            onem2mtc0207011,        // 서버에서 수신
            onem2mtc0207012,        // 단말에서 self 송수신

            onem2mtc0208011,        // 모듈 OFF
            onem2mtc0208012,        // 모듈 ON - POA 업데이트 이벤트 대기

            onem2mtc0209011,        // ACP 1회 생성
            onem2mtc0209012,        // ACP 생성 오류(존재)/삭제후 생성
            onem2mtc0209013,        // ACP 조회오류(미존재)/생성
            onem2mtc020902,         // ACP 조회
            onem2mtc020903,
            onem2mtc0209041,
            onem2mtc0209042,

            onem2mtc021001,
            onem2mtc021002,         // push test는 별도 수동 진행
            onem2mtc0210031,
            onem2mtc0210032,        // DEVICE FW DATA 수신중
            onem2mtc0210033,        // DEVICE FW DATA 수신 완료

            onem2mtc0210034,        // deviceFWUPstart,
            onem2mtc0210035,        // deviceFWDLfinsh,
            onem2mtc0210036,        // deviceFWList,
            onem2mtc0210037,        // deviceFWOpen,
            onem2mtc0210038,        // deviceFWRead,
            onem2mtc0210039,        // deviceFWClose,

            onem2mtc0210041,
            onem2mtc0210042,        // DEVICE version report 준비

            onem2mtc021101,
            onem2mtc021102,         //  push test는 별도 수동 진행
            onem2mtc0211031,
            onem2mtc0211032,        // EC21/EC25 upgrade 이후 oneM2M 모드 설정
            onem2mtc0211033,
            onem2mtc0211034,
            onem2mtc0211035,
            onem2mtc0211040,        // module reset 이후 oneM2M 모드 요청
            onem2mtc0211041,        // module version finsh를 위해 MEF인증 요청
            onem2mtc0211042,        // module version report 요청
            onem2mtc0211043,        // module version 완료
            onem2mtc0211044,        // 결과 확인을 위해 module version read

            //onem2mtc021201,         // 데이터 삭제 시험 불필요
            onem2mtc0212021,         // 구독 등록 존재하여 삭제 후 생성 요청
            onem2mtc0212022,         // 구독 삭제 - 폴더 삭제 시험
            onem2mtc0212031,        // StoD폴더 삭제 - DtoS 폴더삭제
            onem2mtc0212032,        // 폴더 삭제 - remoteCSE 삭제 시험
            onem2mtc0212041,        // remoteCSE 존재하여 삭제 후 생성 요청
            onem2mtc0212042,        // remoteCSE 삭제 (TC 마지막)

            onem2mtc021301,         // 디바이스 데이터 forwarding
            onem2mtc021302,         // Device control data
            onem2mtc0213031,         // Waiting Device Status Check
            onem2mtc0213032,         // Response Device Status Check

            onem2mtc0214011,         // 원격 재부팅 수신
            onem2mtc0214012,         // oneM2M Client 구동 요청
            onem2mtc0214013,         // MEF 인증 요청
            onem2mtc0214014,         // MEF 인증 요청

            onem2mset01,            // remoteCSE 생성
            onem2mset02,            // DtoS 폴더 생성
            onem2mset03,            // StoD 폴더 생성
            onem2mset04,            // StoD 구독 신청
        }

        private enum onem2mtc
        {
            tc020101,
            tc020102,

            tc020201,

            tc020301,

            tc020401,

            tc020501,
            tc020502,
            tc020503,
            tc020504,
            tc020505,

            tc020601,
            tc020602,
            tc020603,
            tc020604,

            tc020701,

            tc020801,

            tc020901,
            tc020902,
            tc020903,
            tc020904,

            tc021001,
            tc021002,
            tc021003,
            tc021004,

            tc021101,
            tc021102,
            tc021103,
            tc021104,

            //tc021201,
            tc021202,
            tc021203,
            tc021204,

            tc021301,
            tc021302,
            tc021303,

            tc021401,

        }

        string svrState = "STOP";

        string dataIN = string.Empty;
        string nextcommand = string.Empty;    //OK를 받은 후 전송할 명령어가 존재하는 경우
                                              //예를들어 +CEREG와 같이 OK를 포함한 응답 값을 받은 경우 OK처리 후에 명령어를 전송해야 한다
                                              // states 값을 바꾸고 명령어를 전송하면 명령의 응답을 받기전 이전에 받았던 OK에 동작할 수 있다.
        string nextcmdexts = string.Empty;
        string nextresponse = string.Empty;   //응답에 prefix가 존재하는 경우
        ServiceServer svr = new ServiceServer();
        Device dev = new Device();
        string beforetcstate = string.Empty;

        UInt32 oneM2Mtotalsize = 0;
        UInt32 oneM2Mrcvsize = 0;
        string filecode = string.Empty;

        string oneM2MMEFIP = "106.103.234.198";
        string oneM2MMEFPort = "80";
        string oneM2MBRKIP = "106.103.234.117";
        string oneM2MBRKPort = "80";
        string oneM2MFOTAIP = "106.103.228.97";
        string oneM2MFOTAPort = "80";

        Dictionary<string, string> commands = new Dictionary<string, string>();
        Dictionary<char, int> bcdvalues = new Dictionary<char, int>();
        Dictionary<string, string> lwm2mtclist = new Dictionary<string, string>();
        Dictionary<string, string> onem2mtclist = new Dictionary<string, string>();

        HttpWebRequest wReq;
        HttpWebResponse wRes;

        string brkUrl = "https://testbrk.onem2m.uplus.co.kr:443"; // BRK(oneM2M 개발기)      
        string mefUrl = "https://testmef.onem2m.uplus.co.kr:443"; // MEF(개발기)
        string logUrl = "http://106.103.228.184/api/v1"; // oneM2M log(개발기)

        DateTime tcStartTime = DateTime.Now.AddHours(-1);
        string tcmsg = string.Empty;
        int httpResCode = 0;
        string httpRSC = string.Empty;

        string altdataid = string.Empty;
        string altfotaid = string.Empty;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bcdvalues.Add('0', 0);
            bcdvalues.Add('1', 1);
            bcdvalues.Add('2', 2);
            bcdvalues.Add('3', 3);
            bcdvalues.Add('4', 4);
            bcdvalues.Add('5', 5);
            bcdvalues.Add('6', 6);
            bcdvalues.Add('7', 7);
            bcdvalues.Add('8', 8);
            bcdvalues.Add('9', 9);
            bcdvalues.Add('A', 10);
            bcdvalues.Add('B', 11);
            bcdvalues.Add('C', 12);
            bcdvalues.Add('D', 13);
            bcdvalues.Add('E', 14);
            bcdvalues.Add('F', 15);
            bcdvalues.Add('a', 10);
            bcdvalues.Add('b', 11);
            bcdvalues.Add('c', 12);
            bcdvalues.Add('d', 13);
            bcdvalues.Add('e', 14);
            bcdvalues.Add('f', 15);

            commands.Add("getimsi", "AT+CIMI");
            commands.Add("geticcid", "AT+ICCID");
            commands.Add("getimei", "AT+GSN");
            commands.Add("geticcidtpb23", "AT+MUICCID");
            commands.Add("geticcidlg", "AT+MUICCID=?");
            commands.Add("geticcidme", "AT+ICCID");
            commands.Add("geticcidamtel", "AT@ICCID?");
            commands.Add("geticcidbc95", "AT+NCCID");
            commands.Add("getimeitpb23", "AT+CGSN=1");
            commands.Add("getmodel", "AT+CGMM");
            commands.Add("getmanufac", "AT+CGMI");
            commands.Add("setcereg", "AT+CEREG=1");
            commands.Add("setceregtpb23", "AT+CEREG=3");
            commands.Add("getcereg", "AT+CEREG?");
            commands.Add("reset", "AT+CFUN=3,3");

            commands.Add("resettpb23", "AT+NRB");

            commands.Add("autogetimsi", "AT+CIMI");
            commands.Add("autogeticcid", "AT+ICCID");
            commands.Add("autogetimei", "AT+GSN");
            commands.Add("autogeticcidtpb23", "AT+MUICCID");
            commands.Add("autogeticcidamtel", "AT@ICCID?");
            commands.Add("autogeticcidme", "AT+ICCID");
            commands.Add("autogeticcidlg", "AT+MUICCID=?");
            commands.Add("autogeticcidbc95", "AT+NCCID");
            commands.Add("autogetimeitpb23", "AT+CGSN=1");
            commands.Add("autogetmodel", "AT+CGMM");
            commands.Add("autogetmodelgmm", "AT+GMM");
            commands.Add("autogetmanufac", "AT+CGMI");

            commands.Add("bootstrap", "AT+QLWM2M=\"bootstrap\",1");
            commands.Add("setserverinfo", "AT+QLWM2M=\"cdp\",");
            commands.Add("setservertype", "AT+QLWM2M=\"select\",2");
            commands.Add("setepns", "AT+QLWM2M=\"epns\",0,\"");
            commands.Add("setmbsps", "AT+QLWM2M=\"mbsps\",\"");
            commands.Add("setAutoBS", "AT+QLWM2M=\"enable\",");
            commands.Add("register", "AT+QLWM2M=\"register\"");
            commands.Add("deregister", "AT+QLWM2M=\"deregister\"");
            commands.Add("lwm2mreset", "AT+QLWM2M=\"reset\"");
            commands.Add("sendmsgstr", "AT+QLWM2M=\"uldata\",10250,");
            commands.Add("sendmsghex", "AT+QLWM2M=\"ulhex\",10250,");
            commands.Add("sendmsgver", "AT+QLWM2M=\"uldata\",26241,");

            commands.Add("disable_bg96", "AT+QLWM2M=\"enable\",0");
            commands.Add("enable_bg96", "AT+QLWM2M=\"enable\",1");
            commands.Add("setcdp_bg96", "AT+QLWM2M=\"cdp\",");

            commands.Add("sendonemsgstr", "AT$OM_C_INS_REQ=");
            commands.Add("sendonemsgsvr", "AT$OM_C_RCIN_REQ=");
            commands.Add("responemsgsvr", "AT$OM_S_RCIN_REQ=");

            commands.Add("getonem2mmode", "AT$LGTMPF?");
            commands.Add("setonem2mmode", "AT$LGTMPF=5");
            commands.Add("setmefauth", "AT$OM_AUTH_REQ=");
            commands.Add("setmefauthnt", "AT$OM_AUTH_REQ=");
            commands.Add("fotamefauthnt", "AT$OM_AUTH_REQ=");
            commands.Add("mfotamefauth", "AT$OM_AUTH_REQ=");
            commands.Add("getCSEbase", "AT$OM_B_CSE_REQ");
            commands.Add("getremoteCSE", "AT$OM_R_CSE_REQ");
            commands.Add("setremoteCSE", "AT$OM_C_CSE_REQ");
            commands.Add("updateremoteCSE", "AT$OM_U_CSE_REQ");
            commands.Add("delremoteCSE", "AT$OM_D_CSE_REQ");
            commands.Add("setcontainer", "AT$OM_C_CON_REQ=DtoS");
            commands.Add("settxcontainer", "AT$OM_C_CON_REQ=StoD");
            commands.Add("delcontainer", "AT$OM_D_CON_REQ=");
            commands.Add("setsubscript", "AT$OM_C_SUB_REQ=");
            commands.Add("delsubscript", "AT$OM_D_SUB_REQ=");
            commands.Add("getonem2mdata", "AT$OM_R_INS_REQ=");
            commands.Add("getACP", "AT$OM_R_ACP_REQ");
            commands.Add("setACP", "AT$OM_C_ACP_REQ=63,*");
            commands.Add("updateACP", "AT$OM_U_ACP_REQ=47,*");
            commands.Add("delACP", "AT$OM_D_ACP_REQ");
            commands.Add("setrcvauto", "AT$OM_MODE=ON");
            commands.Add("setrcvmanu", "AT$OM_MODE=OFF");
            commands.Add("resetreport", "AT$OM_RESET_FINISH");
            commands.Add("resetmefauth", "AT$OM_AUTH_REQ=");

            commands.Add("setserverinfotpb23", "AT+NCDP=");
            commands.Add("setncdp", "AT+NCDP=");
            commands.Add("bootstrapmodetpb23", "AT+MBOOTSTRAPMODE=1");
            commands.Add("setepnstpb23", "AT+MLWEPNS=ASN_CSE-D-");
            commands.Add("setmbspstpb23", "AT+MLWMBSPS=serviceCode=");
            commands.Add("bootstraptpb23", "AT+MLWGOBOOTSTRAP=1");
            commands.Add("registertpb23", "AT+MLWSREGIND=0");
            commands.Add("deregistertpb23", "AT+MLWSREGIND=1");
            commands.Add("registerbc95", "AT+QLWSREGIND=0");
            commands.Add("deregisterbc95", "AT+QLWSREGIND=1");
            commands.Add("lwm2mresettpb23", "AT+FATORYRESET=0");
            commands.Add("sendmsgstrtpb23", "AT+MLWULDATA=");
            commands.Add("sendmsgstbc95", "AT+QLWULDATA=0,");
            commands.Add("sendmsgvertpb23", "AT+MLWULDATA=1,");
            commands.Add("sendmsgverbc95", "AT+QLWULDATA=1,");

            commands.Add("holdoffbc95", "AT+QBOOTSTRAPHOLDOFF=0");
            commands.Add("lwm2mresetbc95", "AT+QREGSWT=2");
            commands.Add("getsvripbc95", "AT+QLWSERVERIP?");
            commands.Add("setsvrbsbc95", "AT+QLWSERVERIP=BS,");
            commands.Add("setsvripbc95", "AT+QLWSERVERIP=LWM2M,");
            commands.Add("autosetsvrbsbc95", "AT+QLWSERVERIP=BS,");
            commands.Add("autosetsvripbc95", "AT+QLWSERVERIP=LWM2M,");
            commands.Add("getepnsbc95", "AT+QLWEPNS?");
            commands.Add("setepnsbc95", "AT+QLWEPNS=0,");
            commands.Add("getmbspsbc95", "AT+QLWMBSPS?");
            commands.Add("setmbspsbc95", "AT+QLWMBSPS=");
            commands.Add("bootstrapbc95", "AT+QREGSWT=1");      // auto (manual=0)
            commands.Add("rebootbc95", "AT+NRB");

            commands.Add("setepnsme", "AT+MLWGENEPNS=");
            commands.Add("sendmsgverme", "AT+MLWDFULDATA=");

            commands.Add("setmefserverinfo", "AT$OM_SVR_INFO=1,");
            commands.Add("sethttpserverinfo", "AT$OM_SVR_INFO=2,");
            commands.Add("setfotaserverinfo", "AT$OM_SVR_INFO=3,");
            commands.Add("getserverinfo", "AT$OM_SVR_INFO?");

            commands.Add("getmodemSvrVer", "AT$OM_MODEM_FWUP_REQ");
            commands.Add("setmodemver", "AT$OM_C_MODEM_FWUP_REQ");
            commands.Add("modemFWUPreport", "AT$OM_MODEM_FWUP_FINISH");
            commands.Add("modemFWUPfinishLTE", "AT$OM_MOD_FWUP_FINISH");
            commands.Add("modemFWUPstart", "AT$OM_MODEM_FWUP_START");

            commands.Add("getdeviceSvrVer", "AT$OM_DEV_FWUP_REQ");
            commands.Add("setdeviceSrvver", "AT$OM_C_DEV_FWUP_REQ");
            commands.Add("deviceFWUPfinish", "AT$OM_DEV_FWUP_FINISH");
            commands.Add("deviceFWUPstart", "AT$OM_DEV_FWUP_START");
            commands.Add("deviceFWList", "AT+QFLST=\"*\"");
            commands.Add("deviceFWOpen", "AT+QFOPEN=");
            commands.Add("deviceFWRead", "AT+QFREAD=");
            commands.Add("deviceFWClose", "AT+QFCLOSE=");

            commands.Add("catm1check","AT+QCFG=\"iotopmode\"");
            commands.Add("catm1set", "AT+QCFG=\"iotopmode\",0");
            commands.Add("catm1apn1", "AT+CGDCONT=1,\"IPV4V6\",\"m2m-catm1.default.lguplus.co.kr\"");
            commands.Add("catm1apn2", "AT+CGDCONT=2");
            commands.Add("catm1psmode", "AT+QCFG=\"servicedomain\",1");
            commands.Add("rfoff", "AT+CFUN=0");
            commands.Add("rfon", "AT+CFUN=1");
            commands.Add("rfreset", "AT+CFUN=1,1");

            commands.Add("catm1imsset", "AT+QCFG=\"iotopmode\",0");
            commands.Add("catm1imsapn1", "AT+CGDCONT=1,\"IPV4V6\",\"m2m-catm1.default.lguplus.co.kr\"");
            commands.Add("catm1imsapn2", "AT+CGDCONT=2,\"IPV4V6\",\"imsv6-m2m.lguplus.co.kr\"");
            commands.Add("catm1imsmode", "AT+QCFG=\"servicedomain\",2");
            commands.Add("catm1imspco", "AT$QCPDPIMSCFGE=2,1,0,1");

            commands.Add("getNWmode", "AT+QCFG=\"iotopmode\"");
            commands.Add("autogetNWmode", "AT+QCFG=\"iotopmode\"");

            commands.Add("nbset", "AT+QCFG=\"iotopmode\",1");
            commands.Add("nbapn1", "AT+CGDCONT=1,\"IPV4V6\",\"\",\"0.0.0.0.0.0.0.0.0.0.0.0.0.0.0.0\",0,0,0,0");
            commands.Add("nbapn2", "AT+CGDCONT=2");
            commands.Add("nbpsmode", "AT+QCFG=\"servicedomain\",1");

            commands.Add("getmodemver", "AT+GMR");
            commands.Add("autogetmodemver", "AT+GMR");
            commands.Add("getmodemverbc95", "AT+CGMR");
            commands.Add("autogetmodemverbc95", "AT+CGMR");
            commands.Add("getmodemvernt", "AT*ST*INFO?");
            commands.Add("autogetmodemvernt", "AT*ST*INFO?");

            /////   디바이스 초기값 설정
            dev.entityId = string.Empty;
            dev.remoteCSEName = string.Empty;
            dev.uuid = string.Empty;
            dev.type = "onem2m";
            dev.model = string.Empty;

            /////   서버 초기값 설정
            svr.enrmtKeyId = string.Empty;
            svr.entityId = string.Empty;

            listView7.View = View.Details;
            listView7.GridLines = true;
            listView7.FullRowSelect = true;
            listView7.CheckBoxes = false;

            listView7.Columns.Add("시간", 60, HorizontalAlignment.Center);
            listView7.Columns.Add("state", 120, HorizontalAlignment.Left);
            listView7.Columns.Add("", 30, HorizontalAlignment.Center);
            listView7.Columns.Add(" 전송 내용", 1000, HorizontalAlignment.Left);

            listView8.View = View.Details;
            listView8.GridLines = true;
            listView8.FullRowSelect = true;
            listView8.CheckBoxes = false;

            listView8.Columns.Add("시간", 55, HorizontalAlignment.Center);
            listView8.Columns.Add("ID", 70, HorizontalAlignment.Center);
            listView8.Columns.Add("이벤트", 120, HorizontalAlignment.Left);
            listView8.Columns.Add("rsltCode", 60, HorizontalAlignment.Center);
            listView8.Columns.Add(" 비고", 800, HorizontalAlignment.Left);

            listView9.View = View.Details;
            listView9.GridLines = true;
            listView9.FullRowSelect = true;
            listView9.CheckBoxes = false;

            listView9.Columns.Add("시간", 55, HorizontalAlignment.Center);
            listView9.Columns.Add("ID", 70, HorizontalAlignment.Center);
            listView9.Columns.Add("이벤트", 120, HorizontalAlignment.Left);
            listView9.Columns.Add("rsltCode", 60, HorizontalAlignment.Center);
            listView9.Columns.Add(" 비고", 800, HorizontalAlignment.Left);

            listView10.View = View.Details;
            listView10.GridLines = true;
            listView10.FullRowSelect = true;
            listView10.CheckBoxes = false;

            listView10.Columns.Add("서버", 60, HorizontalAlignment.Center);
            listView10.Columns.Add("TYPE", 70, HorizontalAlignment.Center);
            listView10.Columns.Add("Method", 100, HorizontalAlignment.Center);
            listView10.Columns.Add(" 상세 내용", 200, HorizontalAlignment.Left);
            listView10.Columns.Add(" 상세 전문", 800, HorizontalAlignment.Left);

            listView11.View = View.Details;
            listView11.GridLines = true;
            listView11.FullRowSelect = true;
            listView11.CheckBoxes = false;

            listView11.Columns.Add("시간", 60, HorizontalAlignment.Center);
            listView11.Columns.Add("state", 120, HorizontalAlignment.Left);
            listView11.Columns.Add("", 30, HorizontalAlignment.Center);
            listView11.Columns.Add(" 전송 내용", 1000, HorizontalAlignment.Left);

            tcStartTime = DateTime.Now.AddHours(-2);
            dateTimePicker1.Value = tcStartTime;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
                dev.type = "lwm2m";
            else
                dev.type = "onem2m";
        }

        private void btnGetLogList_Click(object sender, EventArgs e)
        {
            string kind = "type=onem2m";
            if (comboBox1.SelectedIndex == 1)
                kind = "type=lwm2m";
            if (tbDeviceCTN.Text != string.Empty)
                kind += "&ctn=" + tbDeviceCTN.Text;
            kind += "&from=" + tcStartTime.ToString("yyyyMMddHHmmss");
            getSvrLoglists(kind, "man");
        }

        private void getSvrLoglists(string kind, string mode)
        {
            ReqHeader header = new ReqHeader();
            header.Url = logUrl + "/logs?" + kind;
            header.Method = "GET";
            header.ContentType = "application/json";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "LogList";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            string retStr = GetHttpLog(header, string.Empty);

            if (retStr != string.Empty)
            {
                //LogWriteNoTime(retStr);
                try
                {
                    JArray jarr = JArray.Parse(retStr); //json 객체로

                    listView8.Items.Clear();
                    listView9.Items.Clear();
                    listView10.Items.Clear();
                    foreach (JObject jobj in jarr)
                    {
                        string time = jobj["logTime"].ToString();
                        string logtime = time.Substring(8, 2) + ":" + time.Substring(10, 2) + ":" + time.Substring(12, 2);
                        var pathInfo = jobj["pathInfo"] ?? " ";
                        var trgAddr = jobj["trgAddr"] ?? "";
                        var resType = jobj["resType"] ?? " ";
                        var oprType = jobj["oprType"] ?? " ";

                        string path = pathInfo.ToString();
                        if (path == " ")
                            path = resType.ToString() + " : " + trgAddr.ToString();

                        tcmsg = string.Empty;

                        ListViewItem newitem = new ListViewItem(new string[] { logtime, jobj["logId"].ToString(), tcmsg, jobj["resultCode"].ToString(), jobj["resultCodeName"].ToString() + " (" + path + ")" });
                        listView8.Items.Add(newitem);
                    }

                    if (listView8.Items.Count > 0)
                    {
                        listView8.Items[0].Focused = true;
                        listView8.Items[0].Selected = true;
                    }
                    else if (mode == "man")
                        MessageBox.Show("플랫폼 로그가 존재하지 않습니다.\nCTN을 확인하세요", tbDeviceCTN.Text + " DEVICE 상태 정보");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else if (mode == "man")
                MessageBox.Show("플랫폼 로그가 존재하지 않습니다.\nCTN을 확인하세요", tbDeviceCTN.Text + " DEVICE 상태 정보");
        }

        private void getSvrDetailLog(string tlogid, string kind, string tresultCode, string tresultCodeName)
        {
            label22.Text = "ID : " + tlogid + " 상세내역";

            // oneM2M log server 응답 확인 (resultcode)
            ReqHeader header = new ReqHeader();
            header.Url = logUrl + "/log?logId=" + tlogid;
            header.Method = "GET";
            header.ContentType = "application/json";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "LogDetail";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            string retStr = GetHttpLog(header, string.Empty);

            listView10.Items.Clear();
            if (retStr != string.Empty)
            {
                //LogWriteNoTime(retStr);

                try
                {
                    JArray jarr = JArray.Parse(retStr); //json 객체로

                    foreach (JObject jobj in jarr)
                    {
                        var methodName = jobj["methodName"] ?? " ";
                        var logType = jobj["logType"] ?? " ";
                        var svrType = jobj["svrType"] ?? " ";

                        string message = string.Empty;
                        string msgbody = string.Empty;

                        string logtype = logType.ToString();
                        if (logtype == "API_LOG")            //  서버 API LOG
                        {
                            logtype = "API";
                            var resultCode = jobj["resultCode"] ?? " ";
                            var trgAddr = jobj["trgAddr"] ?? " ";
                            var prtcType = jobj["prtcType"] ?? "";
                            //if (resultCode.ToString() != " ")
                            //    tBResultCode.Text = resultCode.ToString();
                            string protocol = prtcType.ToString();
                            if (protocol != "")
                                protocol = "(" + protocol + ")";

                            message = resultCode.ToString() + " " + protocol;
                            msgbody = trgAddr.ToString();
                        }
                        else if (logtype == "HTTP")
                        {
                            var httpMethod = jobj["httpMethod"] ?? " ";
                            var uri = jobj["uri"] ?? " ";
                            var httpheader = jobj["header"] ?? " ";
                            var body = jobj["body"] ?? " ";
                            var responseBody = jobj["responseBody"] ?? " ";

                            string ntparam = string.Empty;
                            if (kind == "tc021101" || kind == "tc021103")
                            {
                                JObject obj = JObject.Parse(httpheader.ToString());
                                var cid = obj["X-OTA-CID"] ?? " ";
                                var nt = obj["X-OTA-NT"] ?? " ";
                                if (cid.ToString() != " " || nt.ToString() != " ")
                                    ntparam = "CID=" + cid.ToString() + "/NT=" + nt.ToString();

                                var pt = obj["X-OTA-PT"] ?? " ";
                                if (kind == "tc021101")
                                {
                                    tcmsg = "Module FW read";
                                }

                                if (kind == "tc021103")
                                {
                                    string[] values = uri.ToString().Split('/');

                                    if (values[5] == "D")
                                    {
                                        tcmsg = "Device FW read";
                                    }
                                    else
                                    {
                                        tcmsg = "Module FW read";
                                    }
                                }
                            }

                            string bodymsg = ParsingReqBodyMsg(body.ToString(), kind, tlogid, tresultCode, tresultCodeName);
                            string resbodymsg = ParsingResBodyMsg(responseBody.ToString(), kind, tlogid, tresultCode, tresultCodeName, ntparam);

                            message = httpMethod.ToString() + " " + uri.ToString();
                            msgbody = "REQUEST\n" + httpheader + "\n" + bodymsg + "\n\nRESPONSE\n" + responseBody + resbodymsg;

                        }
                        else if (logtype == "HTTP_CLIENT")
                        {
                            logtype = "CLIENT";
                            var responseCode = jobj["responseCode"] ?? " ";
                            string resp = responseCode.ToString();

                            var uri = jobj["uri"] ?? " ";
                            var reqheader = jobj["header"] ?? " ";
                            var responseHeader = jobj["responseHeader"] ?? " ";

                            if (responseHeader.ToString() != " ")
                            {
                                JObject obj = JObject.Parse(responseHeader.ToString());
                                var rsc = obj["X-M2M-RSC"] ?? " ";
                                resp += "/" + rsc.ToString();
                                //var resultcode = obj["X-LGU-RSC"] ?? " ";
                                //if (resultcode.ToString() != " ")
                                //    tBResultCode.Text = resultcode.ToString();
                            }

                            message = resp + " (" + uri.ToString() + ")";
                            msgbody = "REQUEST\n" + reqheader + "\n\nRESPONSE\n" + responseHeader;
                        }
                        else if (logtype == "RUNTIME_LOG")
                        {
                            logtype = "RUN";
                            var topicOrEntityId = jobj["topicOrEntityId"] ?? " ";
                            var requestEntity = jobj["requestEntity"] ?? " ";
                            var responseEntity = jobj["responseEntity"] ?? " ";

                            message = topicOrEntityId.ToString();
                            msgbody = "REQUEST\n" + requestEntity + "\n\nRESPONSE\n" + responseEntity;
                        }

                        string svrtype = svrType.ToString();
                        if (svrtype == "CSE-NB01")
                            svrtype = "CSNB01";

                        string method = methodName.ToString();
                        if (method == "httpClientRuntimeLog")
                            method = "httpClientRuntime";

                        if (method.Length < 8)
                            method += "         ";

                        ListViewItem newitem = new ListViewItem(new string[] { svrtype, logtype, method, message, msgbody});
                        listView10.Items.Add(newitem);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private string ParsingReqBodyMsg(string body, string tckind, string tlogid, string tresultCode, string tresultCodeName)
        {
            string decode = " ";
            string bodymsg = body.Replace("\t", "");
            Console.WriteLine(bodymsg);

            if (bodymsg.StartsWith("{", System.StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    JObject obj = JObject.Parse(bodymsg);
                    string format = string.Empty;
                    string value = string.Empty;

                    if (obj["cnf"] != null)
                    {
                        format = obj["cnf"].ToString(); // data format
                        value = obj["con"].ToString(); // data value
                    }

                    if (value != string.Empty)
                    {
                        if (format == "application/octet-stream")
                        {
                            string hexOutput = string.Empty;
                            string ascii = "YES";
                            byte[] orgBytes = Convert.FromBase64String(value);
                            char[] orgChars = System.Text.Encoding.ASCII.GetString(orgBytes).ToCharArray();
                            foreach (char _eachChar in orgChars)
                            {
                                // Get the integral value of the character.
                                int intvalue = Convert.ToInt32(_eachChar);
                                // Convert the decimal value to a hexadecimal value in string form.
                                if (intvalue < 16)
                                {
                                    hexOutput += "0";
                                    ascii = "NO";
                                }
                                else if (intvalue < 32)
                                {
                                    ascii = "NO";
                                }
                                hexOutput += String.Format("{0:X}", intvalue);
                            }
                            //logPrintInTextBox(hexOutput, "");

                            if (hexOutput != string.Empty)
                            {
                                decode = "\n\n( HEX DATA : " + hexOutput;

                                if (ascii == "YES")
                                {
                                    string asciidata = Encoding.UTF8.GetString(orgBytes);
                                    decode += "\nASCII DATA : " + asciidata;
                                }
                                decode += ")";
                            }
                        }
                        else
                        {
                            decode = "\n\n( DATA : " + value + " )";
                        }
                    }
                    //LogWrite("decode = " + decode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else if (bodymsg.StartsWith("<?xml", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string format = string.Empty;
                string value = string.Empty;

                //bodymsg = bodymsg.Replace("\\t", "");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(bodymsg);
                //logPrintTC(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    try
                    {
                        if (xn["cnf"] != null)
                            format = xn["cnf"].InnerText; // data format
                        if (xn["con"] != null)
                            value = xn["con"].InnerText; // data value

                        if (xn["nev"] != null)
                            if (xn["nev"]["rep"] != null)
                                if (xn["nev"]["rep"]["m2m:cin"] != null)
                                {
                                    if (xn["nev"]["rep"]["m2m:cin"]["cnf"] != null)
                                        format = xn["nev"]["rep"]["m2m:cin"]["cnf"].InnerText; // data format
                                    if (xn["nev"]["rep"]["m2m:cin"]["con"] != null)
                                        value = xn["nev"]["rep"]["m2m:cin"]["con"].InnerText; // data value
                                }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                Console.WriteLine("value = " + value);
                Console.WriteLine("format = " + format);

                if (format == "application/octet-stream")
                {
                    string hexOutput = string.Empty;
                    string ascii = "YES";
                    byte[] orgBytes = Convert.FromBase64String(value);
                    char[] orgChars = System.Text.Encoding.ASCII.GetString(orgBytes).ToCharArray();
                    foreach (char _eachChar in orgChars)
                    {
                        // Get the integral value of the character.
                        int intvalue = Convert.ToInt32(_eachChar);
                        // Convert the decimal value to a hexadecimal value in string form.
                        if (intvalue < 16)
                        {
                            hexOutput += "0";
                            ascii = "NO";
                        }
                        else if (intvalue < 32)
                        {
                            ascii = "NO";
                        }
                        hexOutput += String.Format("{0:X}", intvalue);
                    }
                    //logPrintInTextBox(hexOutput, "");

                    if (hexOutput != string.Empty)
                    {
                        decode = "\n\n( HEX DATA : " + hexOutput;

                        if (ascii == "YES")
                        {
                            string asciidata = Encoding.UTF8.GetString(orgBytes);
                            decode += "\nASCII DATA : " + asciidata;
                        }
                        decode += ")";
                    }
                }
                else if (value != string.Empty)
                {
                    decode = "\n\n( DATA : " + value + " )";
                }
                //LogWrite("decode = " + decode);
            }
            else if (bodymsg.StartsWith("<m2m", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string format = string.Empty;
                string value = string.Empty;

                //bodymsg = bodymsg.Replace("\\t", "");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(bodymsg);
                //logPrintTC(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    try
                    {
                        if (xn["cnf"] != null)
                            format = xn["cnf"].InnerText; // data format
                        if (xn["con"] != null)
                            value = xn["con"].InnerText; // data value
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                //LogWrite("value = " + value);
                //LogWrite("format = " + format);

                if (format == "application/octet-stream")
                {
                    string hexOutput = string.Empty;
                    string ascii = "YES";
                    byte[] orgBytes = Convert.FromBase64String(value);
                    char[] orgChars = System.Text.Encoding.ASCII.GetString(orgBytes).ToCharArray();
                    foreach (char _eachChar in orgChars)
                    {
                        // Get the integral value of the character.
                        int intvalue = Convert.ToInt32(_eachChar);
                        // Convert the decimal value to a hexadecimal value in string form.
                        if (intvalue < 16)
                        {
                            hexOutput += "0";
                            ascii = "NO";
                        }
                        else if (intvalue < 32)
                        {
                            ascii = "NO";
                        }
                        hexOutput += String.Format("{0:X}", intvalue);
                    }
                    //logPrintInTextBox(hexOutput, "");

                    if (hexOutput != string.Empty)
                    {
                        decode = "\n\n( HEX DATA : " + hexOutput;

                        if (ascii == "YES")
                        {
                            string asciidata = Encoding.UTF8.GetString(orgBytes);
                            decode += "\nASCII DATA : " + asciidata;
                        }
                        decode += ")";
                    }
                }
                else if (value != string.Empty)
                {
                    decode = "\n\n( DATA : " + value + " )";
                }
                //LogWrite("decode = " + decode);
            }
            return (bodymsg + decode);
        }

        private string ParsingResBodyMsg(string body, string tckind, string tlogid, string tresultCode, string tresultCodeName, string ntparam)
        {
            string decode = " ";
            string bodymsg = body.Replace("\t", "");
            Console.WriteLine(bodymsg);

            if (bodymsg.StartsWith("{", System.StringComparison.CurrentCultureIgnoreCase))
            {
                try
                {
                    JObject obj = JObject.Parse(bodymsg);
                    string format = string.Empty;
                    string value = string.Empty;

                    if (obj["cnf"] != null)
                    {
                        format = obj["cnf"].ToString(); // data format
                        value = obj["con"].ToString(); // data value
                    }

                    if (value != string.Empty)
                    {
                        if (format == "application/octet-stream")
                        {
                            string hexOutput = string.Empty;
                            string ascii = "YES";
                            byte[] orgBytes = Convert.FromBase64String(value);
                            char[] orgChars = System.Text.Encoding.ASCII.GetString(orgBytes).ToCharArray();
                            foreach (char _eachChar in orgChars)
                            {
                                // Get the integral value of the character.
                                int intvalue = Convert.ToInt32(_eachChar);
                                // Convert the decimal value to a hexadecimal value in string form.
                                if (intvalue < 16)
                                {
                                    hexOutput += "0";
                                    ascii = "NO";
                                }
                                else if (intvalue < 32)
                                {
                                    ascii = "NO";
                                }
                                hexOutput += String.Format("{0:X}", intvalue);
                            }
                            //logPrintInTextBox(hexOutput, "");

                            if (hexOutput != string.Empty)
                            {
                                decode = "\n\n( HEX DATA : " + hexOutput;

                                if (ascii == "YES")
                                {
                                    string asciidata = Encoding.UTF8.GetString(orgBytes);
                                    decode += "\nASCII DATA : " + asciidata;
                                }
                                decode += ")";
                            }
                        }
                        else
                        {
                            decode = "\n\n( DATA : " + value + " )";
                        }
                    }
                    //LogWrite("decode = " + decode);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else if (bodymsg.StartsWith("<?xml", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string format = string.Empty;
                string value = string.Empty;

                //bodymsg = bodymsg.Replace("\\t", "");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(bodymsg);
                //logPrintTC(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    try
                    {
                        if (xn["cnf"] != null)
                            format = xn["cnf"].InnerText; // data format
                        if (xn["con"] != null)
                            value = xn["con"].InnerText; // data value

                        if (xn["nev"] != null)
                            if (xn["nev"]["rep"] != null)
                                if (xn["nev"]["rep"]["m2m:cin"] != null)
                                {
                                    if (xn["nev"]["rep"]["m2m:cin"]["cnf"] != null)
                                        format = xn["nev"]["rep"]["m2m:cin"]["cnf"].InnerText; // data format
                                    if (xn["nev"]["rep"]["m2m:cin"]["con"] != null)
                                        value = xn["nev"]["rep"]["m2m:cin"]["con"].InnerText; // data value
                                }
                        if (tckind == "tc021004")
                        {
                            if (xn["hwty"] != null)
                            {
                                if (xn["hwty"].InnerText == "D")
                                {
                                    tcmsg = "Device FW Report";
                                }
                                else
                                {
                                    tcmsg = "Module FW Report";
                                }
                            }
                            else
                            {
                                tcmsg = "Device FW Report";
                            }
                        }

                        if (tckind == "tc021101")
                        {
                            if (xn["url"] != null)
                            {
                                string url = xn["url"].InnerText;
                                string[] values = url.Split('/');    // 수신한 데이터를 한 문장씩 나누어 array에 저장

                                if (values[3] == "D")
                                {
                                    tcmsg = "Device FW Check";
                                }
                                else
                                {
                                    tcmsg = "Module FW Check";
                                }
                            }
                            else
                            {
                                tcmsg = "Device FW Check";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }

                Console.WriteLine("value = " + value);
                Console.WriteLine("format = " + format);

                if (format == "application/octet-stream")
                {
                    string hexOutput = string.Empty;
                    string ascii = "YES";
                    byte[] orgBytes = Convert.FromBase64String(value);
                    char[] orgChars = System.Text.Encoding.ASCII.GetString(orgBytes).ToCharArray();
                    foreach (char _eachChar in orgChars)
                    {
                        // Get the integral value of the character.
                        int intvalue = Convert.ToInt32(_eachChar);
                        // Convert the decimal value to a hexadecimal value in string form.
                        if (intvalue < 16)
                        {
                            hexOutput += "0";
                            ascii = "NO";
                        }
                        else if (intvalue < 32)
                        {
                            ascii = "NO";
                        }
                        hexOutput += String.Format("{0:X}", intvalue);
                    }
                    //logPrintInTextBox(hexOutput, "");

                    if (hexOutput != string.Empty)
                    {
                        decode = "\n\n( HEX DATA : " + hexOutput;

                        if (ascii == "YES")
                        {
                            string asciidata = Encoding.UTF8.GetString(orgBytes);
                            decode += "\nASCII DATA : " + asciidata;
                        }
                        decode += ")";
                    }
                }
                else if (value != string.Empty)
                {
                    decode = "\n\n( DATA : " + value + " )";
                }
                //LogWrite("decode = " + decode);
            }
            else if (bodymsg.StartsWith("<m2m", System.StringComparison.CurrentCultureIgnoreCase))
            {
                string format = string.Empty;
                string value = string.Empty;

                //bodymsg = bodymsg.Replace("\\t", "");
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(bodymsg);
                //logPrintTC(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    try
                    {
                        if (xn["cnf"] != null)
                            format = xn["cnf"].InnerText; // data format
                        if (xn["con"] != null)
                            value = xn["con"].InnerText; // data value
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }
                }
                //LogWrite("value = " + value);
                //LogWrite("format = " + format);

                if (format == "application/octet-stream")
                {
                    string hexOutput = string.Empty;
                    string ascii = "YES";
                    byte[] orgBytes = Convert.FromBase64String(value);
                    char[] orgChars = System.Text.Encoding.ASCII.GetString(orgBytes).ToCharArray();
                    foreach (char _eachChar in orgChars)
                    {
                        // Get the integral value of the character.
                        int intvalue = Convert.ToInt32(_eachChar);
                        // Convert the decimal value to a hexadecimal value in string form.
                        if (intvalue < 16)
                        {
                            hexOutput += "0";
                            ascii = "NO";
                        }
                        else if (intvalue < 32)
                        {
                            ascii = "NO";
                        }
                        hexOutput += String.Format("{0:X}", intvalue);
                    }
                    //logPrintInTextBox(hexOutput, "");

                    if (hexOutput != string.Empty)
                    {
                        decode = "\n\n( HEX DATA : " + hexOutput;

                        if (ascii == "YES")
                        {
                            string asciidata = Encoding.UTF8.GetString(orgBytes);
                            decode += "\nASCII DATA : " + asciidata;
                        }
                        decode += ")";
                    }
                }
                else if (value != string.Empty)
                {
                    decode = "\n\n( DATA : " + value + " )";
                }
                //LogWrite("decode = " + decode);
            }
            return (bodymsg + decode);
        }

        public string GetHttpLog(ReqHeader header, string data)
        {
            string resResult = string.Empty;

            try
            {
                wReq = (HttpWebRequest)WebRequest.Create(header.Url);
                wReq.Method = header.Method;
                if (header.ContentType != string.Empty)
                    wReq.ContentType = header.ContentType;
                /*
                if (header.X_M2M_RI != string.Empty)
                    wReq.Headers.Add("X-M2M-RI", header.X_M2M_RI);
                if (header.X_M2M_Origin != string.Empty)
                    wReq.Headers.Add("X-M2M-Origin", header.X_M2M_Origin);
                if (header.X_MEF_TK != string.Empty)
                    wReq.Headers.Add("X-MEF-TK", header.X_MEF_TK);
                if (header.X_MEF_EKI != string.Empty)
                    wReq.Headers.Add("X-MEF-EKI", header.X_MEF_EKI);
                */

                LogWrite(wReq.Method + " " + wReq.RequestUri,"T");
                Console.WriteLine(wReq.Method + " " + wReq.RequestUri + " HTTP/1.1");
                Console.WriteLine("");
                for (int i = 0; i < wReq.Headers.Count; ++i)
                    Console.WriteLine(wReq.Headers.Keys[i] + ": " + wReq.Headers[i]);
                Console.WriteLine("");
                Console.WriteLine(data);
                Console.WriteLine("");

                wReq.Timeout = 30000;          // 서버 응답을 30초동안 기다림
                using (wRes = (HttpWebResponse)wReq.GetResponse())
                {
                    LogWrite((int)wRes.StatusCode + " " + wRes.StatusCode.ToString(), "R");
                    Console.WriteLine("HTTP/1.1 " + (int)wRes.StatusCode + " " + wRes.StatusCode.ToString());
                    Console.WriteLine("");
                    for (int i = 0; i < wRes.Headers.Count; ++i)
                        Console.WriteLine("[" + wRes.Headers.Keys[i] + "] " + wRes.Headers[i]);
                    Console.WriteLine("");

                    Stream respPostStream = wRes.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);
                    resResult = readerPost.ReadToEnd();
                    if (resResult.StartsWith("[") || resResult.StartsWith("{"))
                    {
                        string beautifiedJson = JValue.Parse(resResult).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                        Console.WriteLine(beautifiedJson);
                    }
                    else
                        Console.WriteLine(resResult);
                    Console.WriteLine("");
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    LogWrite((int)resp.StatusCode + " " + resp.StatusCode.ToString(), "R");
                    Console.WriteLine("HTTP/1.1 " + (int)resp.StatusCode + " " + resp.StatusCode.ToString());
                    Console.WriteLine("");
                    for (int i = 0; i < resp.Headers.Count; ++i)
                        Console.WriteLine(" " + resp.Headers.Keys[i] + ": " + resp.Headers[i]);
                    Console.WriteLine("");

                    Stream respPostStream = resp.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);
                    string resError = readerPost.ReadToEnd();
                    Console.WriteLine(resError);
                    Console.WriteLine("");
                    Console.WriteLine("[" + (int)resp.StatusCode + "] " + resp.StatusCode.ToString());
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return resResult;
        }

        private void getSvrEventLog(string tlogid, string tresultCode, string tresultCodeName)
        {
            label21.Text = "서버로그 ID : " + tlogid + " 상세내역";

            // oneM2M log server 응답 확인 (resultcode)
            ReqHeader header = new ReqHeader();
            header.Url = logUrl + "/apilog?logId=" + tlogid;
            //header.Url = logUrl + "/apilog?Id=61";
            header.Method = "GET";
            header.ContentType = "application/json";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "LogDetail";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            string retStr = GetHttpLog(header, string.Empty);

            listView9.Items.Clear();
            listView10.Items.Clear();

            if (retStr != string.Empty)
            {
                //LogWriteNoTime(retStr);
                try
                {
                    JArray jarr = JArray.Parse(retStr); //json 객체로

                    foreach (JObject jobj in jarr)
                    {
                        string time = jobj["logTime"].ToString();
                        string logtime = time.Substring(8, 2) + ":" + time.Substring(10, 2) + ":" + time.Substring(12, 2);
                        var pathInfo = jobj["pathInfo"] ?? " ";
                        var resType = jobj["resType"] ?? " ";
                        var trgAddr = jobj["trgAddr"] ?? " ";
                        var logType = jobj["logType"] ?? " ";
                        var logId = jobj["logId"] ?? " ";
                        var resultCode = jobj["resultCode"] ?? " ";
                        var resultCodeName = jobj["resultCodeName"] ?? " ";
                        var oprType = jobj["oprType"] ?? " ";

                        string path = pathInfo.ToString();
                        if (path == " ")
                            path = jobj["resType"].ToString() + " : " + trgAddr.ToString();

                        tcmsg = string.Empty;

                        ListViewItem newitem = new ListViewItem(new string[] { logtime, logId.ToString(), tcmsg, resultCode.ToString(), resultCodeName.ToString() + " (" + logType.ToString() + " => " + path + ")" });
                        listView9.Items.Add(newitem);
                    }

                    if (listView9.Items.Count > 0)
                    {
                        listView9.Items[0].Focused = true;
                        listView9.Items[0].Selected = true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        private void btnMEFAuth_Click(object sender, EventArgs e)
        {
            svr.svcSvrCd = tbSvcSvrCd.Text; // 서비스 서버의 시퀀스
            svr.svcCd = tbSvcCd.Text; // 서비스 서버의 서비스코드
            svr.svcSvrNum = tbSvcSvrNum.Text; // 서비스 서버의 Number
            RequestMEF();
        }

        // 1. MEF 인증
        private void RequestMEF()
        {
            ReqHeader header = new ReqHeader();
            header.Url = mefUrl + "/mef/server";
            header.Method = "POST";
            header.ContentType = "application/xml";
            header.X_M2M_RI = string.Empty;
            header.X_M2M_Origin = string.Empty;
            header.X_MEF_TK = string.Empty;
            header.X_MEF_EKI = string.Empty;
            header.X_M2M_NM = string.Empty;

            XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
            XElement xroot = new XElement("auth");
            xdoc.Add(xroot);

            XElement xparams = new XElement("svcSvrCd", svr.svcSvrCd);
            xroot.Add(xparams);
            xparams = new XElement("svcCd", svr.svcCd);
            xroot.Add(xparams);
            xparams = new XElement("svcSvrNum", svr.svcSvrNum);
            xroot.Add(xparams);
            /*
            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<auth>";
            packetStr += "<svcSvrCd>" + svr.svcSvrCd + "</svcSvrCd>";
            packetStr += "<svcCd>" + svr.svcCd + "</svcCd>";
            packetStr += "<svcSvrNum>" + svr.svcSvrNum + "</svcSvrNum>";
            packetStr += "</auth>";
            */

            string retStr = SendHttpRequest(header, xdoc.ToString()); // xml
            if (retStr != string.Empty)
            {
                ParsingXml(retStr);

                string nameCSR = svr.entityId.Replace("-", "");
                svr.remoteCSEName = "csr-" + nameCSR;
                //LogWrite("svr.remoteCSEName = " + svr.remoteCSEName);
            }
        }

        private void ParsingXml(string xml)
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(xml);
            //LogWrite(xDoc.OuterXml.ToString());

            XmlNodeList xnList = xDoc.SelectNodes("/authdata/http"); //접근할 노드
            foreach (XmlNode xn in xnList)
            {
                svr.enrmtKey = xn["enrmtKey"].InnerText; // oneM2M 인증 KeyID를 생성하기 위한 Key
                svr.entityId = xn["entityId"].InnerText; // oneM2M에서 사용하는 단말 ID
                svr.token = xn["token"].InnerText; // 인증구간 통신을 위해 발급하는 Token
            }
            Console.WriteLine("svr enrmtKey = " + svr.enrmtKey);
            Console.WriteLine("svr entityId = " + svr.entityId);
            Console.WriteLine("svr token = " + svr.token);

            label23.Text = svr.entityId;

            // EKI값 계산하기
            // short uuid구하기
            string[] rx_svrdatas = svr.entityId.Split('-');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
            //string suuid = svr.entityId.Substring(10, 10);
            string suuid = rx_svrdatas[2];
            //LogWrite("suuid = " + suuid);

            // KeyData Base64URL Decoding
            string output = svr.enrmtKey;
            output = output.Replace('-', '+'); // 62nd char of encoding
            output = output.Replace('_', '/'); // 63rd char of encoding

            switch (output.Length % 4) // Pad with trailing '='s
            {
                case 0:
                    break; // No pad chars in this case
                case 2:
                    output += "==";
                    break; // Two pad chars
                case 3:
                    output += "=";
                    break; // One pad char
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(svr.enrmtKey), "Illegal base64url string!");
            }

            var converted = Convert.FromBase64String(output); // Standard base64 decoder

            // keyData로 AES 128비트 비밀키 생성
            System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
            AesManaged tdes = new AesManaged();
            tdes.Key = converted;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform crypt = tdes.CreateEncryptor();
            byte[] plain = Encoding.UTF8.GetBytes(suuid);
            byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
            String enrmtKeyId = Convert.ToBase64String(cipher);

            enrmtKeyId = enrmtKeyId.Split('=')[0]; // Remove any trailing '='s
            enrmtKeyId = enrmtKeyId.Replace('+', '-'); // 62nd char of encoding
            enrmtKeyId = enrmtKeyId.Replace('/', '_'); // 63rd char of encoding

            svr.enrmtKeyId = enrmtKeyId;
            //LogWrite("svr.enrmtKeyId = " + svr.enrmtKeyId);
        }

        delegate void Ctr_Involk(Control ctr, string text);

        private void SetText(Control ctr, string txtValue)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctr.InvokeRequired)
            {
                Ctr_Involk CI = new Ctr_Involk(SetText);
                ctr.Invoke(CI, ctr, txtValue);
            }
            else
            {
                ctr.Text = txtValue;
            }
        }

        public string SendHttpRequest(ReqHeader header, string data)
        {
            string resResult = string.Empty;

            try
            {
                wReq = (HttpWebRequest)WebRequest.Create(header.Url);
                wReq.Method = header.Method;
                if (header.Accept != string.Empty)
                    wReq.Accept = header.Accept;
                if (header.ContentType != string.Empty)
                    wReq.ContentType = header.ContentType;
                if (header.X_M2M_RI != string.Empty)
                    wReq.Headers.Add("X-M2M-RI", header.X_M2M_RI);
                if (header.X_M2M_Origin != string.Empty)
                    wReq.Headers.Add("X-M2M-Origin", header.X_M2M_Origin);
                if (header.X_M2M_NM != string.Empty)
                    wReq.Headers.Add("X-M2M-NM", header.X_M2M_NM);
                if (header.X_MEF_TK != string.Empty)
                    wReq.Headers.Add("X-MEF-TK", header.X_MEF_TK);
                if (header.X_MEF_EKI != string.Empty)
                    wReq.Headers.Add("X-MEF-EKI", header.X_MEF_EKI);

                LogWrite(wReq.Method + " " + wReq.RequestUri,"T");
                Console.WriteLine(wReq.Method + " " + wReq.RequestUri + " HTTP/1.1");
                Console.WriteLine("");
                for (int i = 0; i < wReq.Headers.Count; ++i)
                    Console.WriteLine(wReq.Headers.Keys[i] + ": " + wReq.Headers[i]);
                Console.WriteLine("");
                Console.WriteLine(data);
                Console.WriteLine("");

                if (data != string.Empty)
                {
                    LogWriteNoTime(data);

                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    Stream dataStream = wReq.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }
                else
                    wReq.ContentLength = 0;

                wReq.Timeout = 20000;          // 서버 응답을 20초동안 기다림
                using (wRes = (HttpWebResponse)wReq.GetResponse())
                {
                    LogWrite((int)wRes.StatusCode + " " + wRes.StatusCode.ToString(),"R");
                    Console.WriteLine("HTTP/1.1 " + (int)wRes.StatusCode + " " + wRes.StatusCode.ToString());
                    Console.WriteLine("");
                    for (int i = 0; i < wRes.Headers.Count; ++i)
                        Console.WriteLine("[" + wRes.Headers.Keys[i] + "] " + wRes.Headers[i]);
                    Console.WriteLine("");

                    Stream respPostStream = wRes.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);
                    resResult = readerPost.ReadToEnd();
                    if (resResult.StartsWith("<?xml"))
                    {
                        XmlDocument xDoc = new XmlDocument();
                        xDoc.LoadXml(resResult);
                        StringWriter writer = new StringWriter();
                        xDoc.Save(writer);
                        Console.WriteLine(writer.ToString());
                    }
                    else if (resResult.StartsWith("{") || resResult.StartsWith("["))
                    {
                        string beautifiedJson = JValue.Parse(resResult).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                        Console.WriteLine(beautifiedJson);
                    }
                    else
                        Console.WriteLine(resResult);
                    Console.WriteLine("");
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    LogWrite((int)resp.StatusCode + " " + resp.StatusCode.ToString(),"R");
                    Console.WriteLine("HTTP/1.1 " + (int)resp.StatusCode + " " + resp.StatusCode.ToString());
                    Console.WriteLine("");
                    for (int i = 0; i < resp.Headers.Count; ++i)
                        Console.WriteLine(" " + resp.Headers.Keys[i] + ": " + resp.Headers[i]);
                    Console.WriteLine("");

                    Stream respPostStream = resp.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);
                    string resError = readerPost.ReadToEnd();
                    Console.WriteLine(resError);
                    Console.WriteLine("");
                    //Console.WriteLine("[" + (int)resp.StatusCode + "] " + resp.StatusCode.ToString());
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return resResult;
        }

        private void LogWrite(string data, string dir)
        {
            BeginInvoke(new Action(() =>
            {
                string time = DateTime.Now.ToString("hh:mm:ss");

                ListViewItem newitem = new ListViewItem(new string[] { time, dir, data });
                listView7.Items.Add(newitem);
                if (listView7.Items.Count > 35)
                    listView7.TopItem = listView7.Items[listView7.Items.Count - 1];
            }));
        }

        private void LogWriteNoTime(string data)
        {
            BeginInvoke(new Action(() =>
            {
                /*
                tbLog.AppendText(Environment.NewLine);
                if (data.StartsWith("<?xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(data);
                    StringWriter writer = new StringWriter();
                    xDoc.Save(writer);
                    tbLog.AppendText(writer.ToString());
                }
                else
                    tbLog.AppendText(" " + data);
                tbLog.SelectionStart = tbLog.TextLength;
                tbLog.ScrollToCaret();
                */

                ListViewItem newitem = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, data });
                listView7.Items.Add(newitem);
                if (listView7.Items.Count > 35)
                    listView7.TopItem = listView7.Items[listView7.Items.Count - 1];
            }));
        }

        private void button127_Click(object sender, EventArgs e)
        {
            if (svr.entityId != string.Empty)
                getSvrLoglists("entityId=" + svr.entityId, "man");
            else
                MessageBox.Show("서비스서버 MEF인증 후 사용이 가능합니다");
        }

        private void button126_Click(object sender, EventArgs e)
        {
            ReqHeader header = new ReqHeader();
            header.Url = logUrl + "/resultCode?value=" + tBResultCode.Text;
            header.Method = "GET";
            header.ContentType = "application/json";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "ResultCode";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            string retStr = GetHttpLog(header, string.Empty);
            if (retStr != string.Empty)
            {
                //LogWriteNoTime(retStr);
                try
                {
                    JObject obj = JObject.Parse(retStr);

                    var resultCode = obj["resultCode"] ?? tBResultCode.Text;
                    var codeName = obj["codeName"] ?? "NULL";
                    var desc = obj["desc"] ?? "NULL";

                    MessageBox.Show("message = " + codeName.ToString() + "\ndescription = " + desc.ToString(), "Resultcode=" + resultCode.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            else
                MessageBox.Show("message = " + "Unknown" + "\ndescription = " + "Resultcode 값이 존재하지 않습니다.", "Resultcode=" + tBResultCode.Text);
        }

        private void button124_Click(object sender, EventArgs e)
        {
                if (tbDeviceCTN.Text != string.Empty)
                {
                    ReqHeader header = new ReqHeader();
                    header.Url = logUrl + "/device?ctn=" + tbDeviceCTN.Text;
                    //header.Url = logUrl + "/device?ctn=99977665825";
                    header.Method = "GET";
                    header.ContentType = "application/json";
                    header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "DeviceGet";
                    header.X_M2M_Origin = svr.entityId;
                    header.X_MEF_TK = svr.token;
                    header.X_MEF_EKI = svr.enrmtKeyId;
                    string retStr = GetHttpLog(header, string.Empty);
                    if (retStr != string.Empty)
                    {
                        //LogWriteNoTime(retStr);
                        try
                        {
                            JArray jarr = JArray.Parse(retStr); //json 객체로

                            JObject obj = JObject.Parse(jarr[0].ToString());

                            var ctn = obj["ctn"] ?? tbDeviceCTN.Text;
                            var deviceModel = obj["deviceModel"] ?? " ";
                            var modemModel = obj["modemModel"] ?? " ";
                            var serviceCode = obj["serviceCode"] ?? " ";
                            var deviceSerialNo = obj["deviceSerialNo"] ?? " ";
                            var iccId = obj["iccId"] ?? " ";
                            var m2mmType = obj["m2mmType"] ?? " ";
                    }
                    catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                            MessageBox.Show("DEVICE 정보가 존재하지 않습니다.\nhttps://testadm.onem2m.uplus.co.kr:8443 에서 확인바랍니다.", tbDeviceCTN.Text + " DEVICE 상태 정보");
                        }
                    }
                    else
                        MessageBox.Show("DEVICE 정보가 존재하지 않습니다.\nhttps://testadm.onem2m.uplus.co.kr:8443 에서 확인바랍니다.", tbDeviceCTN.Text + " DEVICE 상태 정보");
                }
                else
                    MessageBox.Show("CTN 정보가 없습니다.\nCTN을 확인하세요");
        }

        private void setDeviceEntityID()
        {
            if (dev.imsi != null && dev.imsi.Length == 11)
            {
                String md5value = getMd5Hash(dev.imsi + dev.iccid);
                //logPrintInTextBox(md5value, "");
                dev.uuid = md5value;

                string epn = md5value.Substring(0, 5) + md5value.Substring(md5value.Length - 5, 5);
                string entityid = "ASN_CSE-D-" + epn + "-" + tbSvcCd.Text;

                if (dev.entityId != entityid)
                {
                    dev.entityId = entityid;
                }
            }
            else
                MessageBox.Show("CTN이 등록되어 있지 않습니다. 확인이 필요합니다.");
        }

        // Hash an input string and return the hash as
        // a 32 character hexadecimal string.
        static string getMd5Hash(string input)
        {
            // Create a new instance of the MD5CryptoServiceProvider object.
            MD5 md5Hasher = MD5.Create();

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        private void button123_Click(object sender, EventArgs e)
        {
            getSvrEventLog(textBox95.Text, string.Empty, string.Empty);
        }

        private void button122_Click(object sender, EventArgs e)
        {
            getSvrDetailLog(textBox94.Text, string.Empty, string.Empty, string.Empty);
        }

        private void btnDeviceCheck_Click(object sender, EventArgs e)
        {
            if (svr.enrmtKeyId != string.Empty)
            {
                RetriveDverToPlatform();
                RetriveMverToPlatform();
            }
            else
                MessageBox.Show("서버인증파라미터 세팅하세요");
        }

        private void RetriveDverToPlatform()
        {
            ReqHeader header = new ReqHeader();
            //header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_01222990847";
            if (comboBox2.SelectedIndex == 1)
                header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + dev.imsi + "/nod-m2m_" + dev.imsi + "/fwr-m2m_D" + dev.imsi;
            else
                header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + dev.imsi + "/nod-m2m_" + dev.imsi + "/fwr-m2m_D_" + dev.imsi;
            header.Method = "GET";
            header.X_M2M_Origin = svr.entityId;
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "device_CSR_retrive";
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.Accept = "application/xml";
            header.ContentType = string.Empty;

            string retStr = SendHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {
                string value = string.Empty;

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(retStr);
                //LogWrite(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    value = xn["vr"].InnerText; // data value
                }
                lbdevicever.Text = value;
            }
        }

        private void RetriveMverToPlatform()
        {
            ReqHeader header = new ReqHeader();
            //header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_01222990847";
            if (comboBox2.SelectedIndex == 0)
                header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + dev.imsi + "/nod-m2m_" + dev.imsi + "/fwr-m2m_" + dev.imsi;
            else if (comboBox2.SelectedIndex == 1)
                header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + dev.imsi + "/nod-m2m_" + dev.imsi + "/fwr-m2m_M" + dev.imsi;
            else
                header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + dev.imsi + "/nod-m2m_" + dev.imsi + "/fwr-m2m_M_" + dev.imsi;
            header.Method = "GET";
            header.X_M2M_Origin = svr.entityId;
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "device_CSR_retrive";
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.Accept = "application/xml";
            header.ContentType = string.Empty;

            string retStr = SendHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {
                string value = string.Empty;

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(retStr);
                //LogWrite(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    value = xn["vr"].InnerText; // data value
                }
                lbmodemfwrver.Text = value;
            }
        }

        private void tbSvcCd_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dev.entityId != string.Empty)
                    setDeviceEntityID();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void btnGetRemoteCSE_Click(object sender, EventArgs e)
        {
            if (svr.enrmtKeyId != string.Empty)
                ReqRemoteCSEGet();
            else
                MessageBox.Show("서버인증파라미터 세팅하세요");
        }

        private void ReqRemoteCSEGet()
        {
            ReqHeader header = new ReqHeader();
            header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/" + svr.remoteCSEName;
            header.Method = "GET";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Retrieve";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            string retStr = SendHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
                LogWriteNoTime(retStr);
        }

        private void btnSetRemoteCSE_Click(object sender, EventArgs e)
        {
            if (svr.enrmtKeyId != string.Empty)
                ReqRemoteCSECreate();
            else
                MessageBox.Show("서버인증파라미터 세팅하세요");
        }

        // 3. RemoteCSE-Create
        private void ReqRemoteCSECreate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1";
            header.Method = "POST";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=16";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Create";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = svr.remoteCSEName;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:csr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<cst>3</cst>";
            packetStr += "<csi>/" + svr.entityId + "</csi>";
            packetStr += "<cb>/" + svr.entityId + "/cb-1</cb>";
            packetStr += "<rr>true</rr>";
            packetStr += "<poa>" + tbSeverIP.Text + ":" + tbSeverPort.Text + "</poa>";
            packetStr += "</m2m:csr>";

            string retStr = SendHttpRequest(header, packetStr);
            //if (retStr != string.Empty)
            //    LogWrite(retStr);
        }

        private void btnDelRemoteCSE_Click(object sender, EventArgs e)
        {
            if (svr.enrmtKeyId != string.Empty)
                ReqRemoteCSEDEL();
            else
                MessageBox.Show("서버인증파라미터 세팅하세요");
        }

        // 3. RemoteCSE-Delete
        private void ReqRemoteCSEDEL()
        {
            ReqHeader header = new ReqHeader();
            header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/" + svr.remoteCSEName;
            header.Method = "DELETE";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Delete";
            header.X_M2M_Origin = svr.entityId;
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            string retStr = SendHttpRequest(header, string.Empty);
            //if (retStr != string.Empty)
            //    LogWrite(retStr);
        }

        private void RetriveDataToPlatform()
        {
            ReqHeader header = new ReqHeader();
            //header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_01222990847/cnt-TEMP/la";
            header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + tbDeviceCTN.Text + "/cnt-DtoS/la";
            header.Method = "GET";
            header.X_M2M_Origin = svr.entityId;
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "data_retrive";
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.Accept = "application/xml";
            header.ContentType = string.Empty;

            string retStr = SendHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {
                string format = string.Empty;
                string value = string.Empty;

                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(retStr);
                //LogWrite(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/*"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    format = xn["cnf"].InnerText; // data format
                    value = xn["con"].InnerText; // data value
                }
                //LogWrite("value = " + value);
                //LogWrite("format = " + format);
            }
        }

        private void SendDataToPlatform()
        {
            ReqHeader header = new ReqHeader();

            header.Url = brkUrl + "/IN_CSE-BASE-1/cb-1/csr-m2m_" + tbDeviceCTN.Text + "/cnt-StoD";
            header.Method = "POST";
            header.X_M2M_Origin = svr.entityId;
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "data_send";
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=4";

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:cin xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<cnf>text/plain</cnf>";

            string txData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " server";
            packetStr += "<con>" + txData + "</con>";
            packetStr += "</m2m:cin>";

            string retStr = SendHttpRequest(header, packetStr);
            //if (retStr != string.Empty)
            //    LogWrite(retStr);
        }

        private void button94_Click(object sender, EventArgs e)
        {
            if (svr.enrmtKeyId != string.Empty)
            {
                if (dev.entityId != String.Empty)
                {
                    rTh = new Thread(new ThreadStart(SendDataToOneM2M));
                    rTh.Start();
                }
                else
                    MessageBox.Show("CTN이 등록되어 있지 않습니다.확인이 필요합니다.");
            }
            else
                MessageBox.Show("서버인증파라미터 세팅하세요");
        }

        private void SendDataToOneM2M()
        {
            ReqHeader header = new ReqHeader();
            setDeviceEntityID();
            header.Url = brkUrl + "/" + dev.entityId + "/TEST";

            header.Method = "POST";
            header.X_M2M_Origin = svr.entityId;
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "data_send";
            header.X_MEF_TK = svr.token;
            header.X_MEF_EKI = svr.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=4";

            string packetStr = "<m2m:cin xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<cnf>text/plain</cnf>";
            string txData = string.Empty;
            txData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " server";
            packetStr += "<con>" + txData + "</con>";
            packetStr += "</m2m:cin>";

            SetText(label2, txData);

            string retStr = SendHttpRequest(header, packetStr);
            //if (retStr != string.Empty)
            //    LogWrite(retStr);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            Console.WriteLine(webBrowser1.Url.ToString());
            try
            {
                if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/login")
                {
                    webBrowser1.Document.Focusing += new HtmlElementEventHandler(Document_Click);

                    string filePath = Application.StartupPath + @"\configure.txt";
                    if (File.Exists(filePath))
                    {
                        FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                        // Open a file to read to.
                        StreamReader sr = new StreamReader(fs);

                        string rddata = sr.ReadLine();
                        webBrowser1.Document.GetElementById("txtId").SetAttribute("value", rddata);

                        rddata = sr.ReadLine();
                        byte[] rdbyte = Convert.FromBase64String(rddata);
                        webBrowser1.Document.GetElementById("txtPassword").SetAttribute("value", Encoding.UTF8.GetString(rdbyte));

                        sr.Close();
                        fs.Close();

                        webBrowser1.Document.GetElementById("btnLogin").InvokeMember("Click");
/*
                        HtmlElementCollection searchBox = webBrowser1.Document.GetElementsByTagName("BUTTON");
                        foreach (HtmlElement el in searchBox)
                        {
                            if (el.GetAttribute("type").Equals("submit"))
                            {
                                el.InvokeMember("Click");
                            }
                        }
  */
                        }
                }
                else if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/login/twofactor")
                {
                    webBrowser1.Document.GetElementById("smsNum").SetAttribute("value", "1234");

                    webBrowser1.Document.GetElementById("btnConfSms").InvokeMember("Click");
/*
                    HtmlElementCollection searchBox = webBrowser1.Document.GetElementsByTagName("BUTTON");
                    foreach (HtmlElement el in searchBox)
                    {
                        if (el.GetAttribute("type").Equals("submit"))
                        {
                            el.InvokeMember("Click");
                        }
                    }
*/
                }
                else if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/logging/realtime")
                {
                    webBrowser1.Navigate("https://testadm.onem2m.uplus.co.kr:8443/terminal");
                }
                else if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/terminal")
                {
                    if (dev.uuid != string.Empty)
                    {
                        webBrowser1.Document.GetElementById("txtEsn").SetAttribute("value", dev.imsi);
/*
                        HtmlElementCollection searchBox = webBrowser1.Document.GetElementById("selCommonKey").GetElementsByTagName("SELECT");
                        foreach (HtmlElement el in searchBox)
                        {
                            if (el.GetAttribute("value").Equals("50"))
                            {
                                el.InvokeMember("Selected");
                            }
                        }
*/
                        webBrowser1.Document.GetElementById("btnSearch").InvokeMember("Click");
                    }
                }
                else if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/deviceMgmt")
                {
                    webBrowser1.Document.GetElementById("txtEntId").SetAttribute("value", dev.entityId);
                    webBrowser1.Document.GetElementById("btnSearch").InvokeMember("Click");
                }
                else if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/firmware")
                {
                    webBrowser1.Document.GetElementById("btnSearch").InvokeMember("Click");
                }
                else if (webBrowser1.Url.ToString() == "https://testadm.onem2m.uplus.co.kr:8443/firmware/modem")
                {
                    webBrowser1.Document.GetElementById("btnSearch").InvokeMember("Click");
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Document_Click(object sender, HtmlElementEventArgs e)
        {
            if (webBrowser1.Document.ActiveElement.TagName == "BUTTON")
            {
                if(webBrowser1.Document.GetElementById("txtId") != null)
                {
                    dynamic ee = webBrowser1.Document.GetElementById("txtId").DomElement;
                    dynamic dd = webBrowser1.Document.GetElementById("txtPassword").DomElement;
                    
                    try
                    {
                        FileStream fs = new FileStream(Application.StartupPath + @"\configure.txt", FileMode.Create, FileAccess.Write);
                        // Create a file to write to.
                        StreamWriter sw = new StreamWriter(fs);

                        sw.WriteLine(ee.value);
                        byte[] ddbyte = System.Text.Encoding.UTF8.GetBytes(dd.value);
                        sw.WriteLine(Convert.ToBase64String(ddbyte));

                        sw.Close();
                        fs.Close();
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            tcStartTime = dateTimePicker1.Value;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "webpage")
            {
                if (webBrowser1.Url.ToString() == "about:blank")
                    webBrowser1.Navigate("https://testadm.onem2m.uplus.co.kr:8443");
            }
            else if (tabControl1.SelectedTab.Name == "tabServer")
            {
                if (listView7.Items.Count > 35)
                    listView7.TopItem = listView7.Items[listView7.Items.Count - 1];
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string pathname = Application.StartupPath + @"/TestResult/";
            DateTime currenttime = DateTime.Now;
            string filename = null;

            Directory.CreateDirectory(pathname);
            pathname += tBoxDeviceModel.Text + "_" + tBoxDeviceModel.Text + "_";

            filename = "TestResult_" + currenttime.ToString("MMdd_hhmmss") + ".xls";
            resultFileWrite(pathname, filename);
        }

        private void resultFileWrite(string pathname, string filename)
        {
            try
            {
                Workbook workbook = new Workbook();
                Worksheet worksheet = new Worksheet("information");

                worksheet.Cells[0, 0] = new Cell("모델명");
                worksheet.Cells[0, 1] = new Cell(dev.model);
                worksheet.Cells[1, 0] = new Cell("제조사");
                worksheet.Cells[1, 1] = new Cell(dev.maker);
                worksheet.Cells[2, 0] = new Cell("버전");
                worksheet.Cells[2, 1] = new Cell(dev.version);
                worksheet.Cells[3, 0] = new Cell("시험일");
                worksheet.Cells[3, 1] = new Cell(DateTime.Now.ToString("MM/dd hh:mm"));
                worksheet.Cells[4, 0] = new Cell("EntityID");
                worksheet.Cells[4, 1] = new Cell(dev.entityId);
                worksheet.Cells[5, 0] = new Cell("CellID");
                worksheet.Cells[5, 1] = new Cell(label49.Text);
                worksheet.Cells.ColumnWidth[0] = 3000;
                worksheet.Cells.ColumnWidth[1] = 10000;
                workbook.Worksheets.Add(worksheet);

                int i;

                worksheet = new Worksheet("server");
                worksheet.Cells[0, 0] = new Cell("시간");
                worksheet.Cells[0, 1] = new Cell("state");
                worksheet.Cells[0, 2] = new Cell(" ");
                worksheet.Cells[0, 3] = new Cell("내용");

                for (i = 0; i < listView7.Items.Count; i++)
                {
                    worksheet.Cells[i + 1, 0] = new Cell(listView7.Items[i].SubItems[0].Text);
                    worksheet.Cells[i + 1, 1] = new Cell(listView7.Items[i].SubItems[1].Text);
                    worksheet.Cells[i + 1, 2] = new Cell(listView7.Items[i].SubItems[2].Text);
                    worksheet.Cells[i + 1, 3] = new Cell(listView7.Items[i].SubItems[3].Text);
                }

                worksheet.Cells.ColumnWidth[0] = 2500;
                worksheet.Cells.ColumnWidth[1] = 5000;
                worksheet.Cells.ColumnWidth[2] = 700;
                worksheet.Cells.ColumnWidth[3] = 30000;
/*
                i = 1;
                // convert string to stream
                byte[] byteArray = Encoding.UTF8.GetBytes(tbLog.Text);
                MemoryStream stream = new MemoryStream(byteArray);

                // convert stream to string
                StreamReader reader = new StreamReader(stream);
                string line = string.Empty;
                while ((line = reader.ReadLine()) != null)
                {
                    worksheet.Cells[i, 0] = new Cell(line);
                    i++;
                }

                worksheet.Cells.ColumnWidth[0] = 11000;
*/
                workbook.Worksheets.Add(worksheet);

                worksheet = new Worksheet("platform");
                worksheet.Cells[0, 0] = new Cell("시간");
                worksheet.Cells[0, 1] = new Cell("ID");
                worksheet.Cells[0, 2] = new Cell("이벤트");
                worksheet.Cells[0, 3] = new Cell("resultCode");
                worksheet.Cells[0, 4] = new Cell("비고");

                for (i = 0; i < listView8.Items.Count; i++)
                {
                    worksheet.Cells[i + 1, 0] = new Cell(listView8.Items[i].SubItems[0].Text);
                    worksheet.Cells[i + 1, 1] = new Cell(listView8.Items[i].SubItems[1].Text);
                    worksheet.Cells[i + 1, 2] = new Cell(listView8.Items[i].SubItems[2].Text);
                    worksheet.Cells[i + 1, 3] = new Cell(listView8.Items[i].SubItems[3].Text);
                    worksheet.Cells[i + 1, 4] = new Cell(listView8.Items[i].SubItems[4].Text);
                }

                worksheet.Cells.ColumnWidth[0] = 2500;
                worksheet.Cells.ColumnWidth[1] = 3000;
                worksheet.Cells.ColumnWidth[2] = 6000;
                worksheet.Cells.ColumnWidth[3] = 3000;
                worksheet.Cells.ColumnWidth[4] = 20000;
                workbook.Worksheets.Add(worksheet);

                workbook.Save(pathname + filename);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listView8_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemColl = listView8.SelectedItems;
            foreach (ListViewItem item in itemColl)
            {
                textBox95.Text = item.SubItems[1].Text;
                tBResultCode.Text = item.SubItems[2].Text;

                getSvrEventLog(item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text);
            }
        }

        private void listView9_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemColl = listView9.SelectedItems;
            foreach (ListViewItem item in itemColl)
            {
                textBox94.Text = item.SubItems[1].Text;
                tBResultCode.Text = item.SubItems[3].Text;

                getSvrDetailLog(item.SubItems[1].Text, string.Empty, item.SubItems[3].Text, item.SubItems[4].Text);
            }
        }

        private void listView10_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemColl = listView10.SelectedItems;
            foreach (ListViewItem item in itemColl)
            {
                if (item.SubItems[4].Text != " ")
                    MessageBox.Show(item.SubItems[3].Text + "\n\n" + item.SubItems[4].Text, "상세내역");
            }
        }

        private void listView7_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemColl = listView7.SelectedItems;
            foreach (ListViewItem item in itemColl)
            {
                string data = item.SubItems[3].Text;
                if (data.StartsWith("<?xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(data);
                    StringWriter writer = new StringWriter();
                    xDoc.Save(writer);
                    MessageBox.Show(writer.ToString(), "상세 내용");
                }
                else if (data.StartsWith("{") || data.StartsWith("["))
                {
                    string beautifiedJson = JValue.Parse(data).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                    MessageBox.Show(beautifiedJson, "상세 내용");
                }
            }
        }

        private void button44_Click_1(object sender, EventArgs e)
        {
            oneM2MMefAuth(tbSvcCd.Text, tBoxDeviceModel.Text, tBoxDeviceVer.Text, tBoxDeviceSN.Text);
        }

        private void oneM2MMefAuth(string svcCode, string model, string version, string serialNo)
        {

            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MMEFIP + ":" + oneM2MMEFPort + "/mef";
            header.Method = "POST";
            header.ContentType = "application/xml";
            header.X_M2M_RI = string.Empty;
            header.X_M2M_Origin = string.Empty;
            header.X_MEF_TK = string.Empty;
            header.X_MEF_EKI = string.Empty;
            header.X_M2M_NM = string.Empty;

            string bodymsg = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><auth><deviceModel>";
            bodymsg += model;
            bodymsg += "</deviceModel><deviceSerialNo>";
            bodymsg += serialNo;
            bodymsg += "</deviceSerialNo><serviceCode>";
            bodymsg += svcCode;
            bodymsg += "</serviceCode><deviceType>asn</deviceType><ctn>";
            bodymsg += tbDeviceCTN.Text;
            bodymsg += "</ctn><iccId>";
//            string iccid = lbIccid.Text;
//            if (iccid.Length > 6)
//                bodymsg += iccid.Substring(iccid.Length - 6, 6);
//            else
                bodymsg += "000000";
            bodymsg += "</iccId><mac></mac></auth>";
            /*
                        XDocument xdoc = new XDocument(new XDeclaration("1.0", "UTF-8", null));
                        XElement xroot = new XElement("auth");
                        xdoc.Add(xroot);

                        XElement xparams = new XElement("deviceModel", model);
                        xroot.Add(xparams);
                        xparams = new XElement("deviceSerialNo", serialNo);
                        xroot.Add(xparams);
                        xparams = new XElement("serviceCode", svcCode);
                        xroot.Add(xparams);
                        xparams = new XElement("deviceType", "apn");
                        xroot.Add(xparams);
                        xparams = new XElement("mac", string.Empty);
                        xroot.Add(xparams);
                        xparams = new XElement("ctn", tbDeviceCTN.Text);
                        xroot.Add(xparams);
                        string iccid = lbIccid.Text;
                        if (iccid.Length > 6)
                            xparams = new XElement("iccid", iccid.Substring(iccid.Length - 6, 6));
                        else
                            xparams = new XElement("iccid", "000000");
                        xroot.Add(xparams);
                        xparams = new XElement("useLongUuid", "false");
                        xroot.Add(xparams);

                        StringWriter writer = new StringWriter();
                        Console.WriteLine(xdoc.Declaration.Encoding);
                        xdoc.Save(writer);
                        Console.WriteLine(writer.ToString());

                        string retStr = DeviceHttpRequest(header, writer.ToString()); // xml
            */
            string retStr  = DeviceHttpRequest(header, bodymsg);
            if (retStr != string.Empty)
            {
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(retStr);
                //LogWrite(xDoc.OuterXml.ToString());

                XmlNodeList xnList = xDoc.SelectNodes("/authdata/http"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    dev.enrmtKey = xn["enrmtKey"].InnerText; // oneM2M 인증 KeyID를 생성하기 위한 Key
                    dev.entityId = xn["entityId"].InnerText; // oneM2M에서 사용하는 단말 ID
                    dev.token = xn["token"].InnerText; // 인증구간 통신을 위해 발급하는 Token
                }

                // EKI값 계산하기
                // short uuid구하기
                string[] rx_svrdatas = dev.entityId.Split('-');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                                                                   //string suuid = svr.entityId.Substring(10, 10);
                string suuid = rx_svrdatas[2];
                //LogWrite("suuid = " + suuid);

                // KeyData Base64URL Decoding
                string output = dev.enrmtKey;
                output = output.Replace('-', '+'); // 62nd char of encoding
                output = output.Replace('_', '/'); // 63rd char of encoding

                switch (output.Length % 4) // Pad with trailing '='s
                {
                    case 0:
                        break; // No pad chars in this case
                    case 2:
                        output += "==";
                        break; // Two pad chars
                    case 3:
                        output += "=";
                        break; // One pad char
                    default:
                        throw new ArgumentOutOfRangeException(
                            nameof(dev.enrmtKey), "Illegal base64url string!");
                }

                var converted = Convert.FromBase64String(output); // Standard base64 decoder

                // keyData로 AES 128비트 비밀키 생성
                System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();
                AesManaged tdes = new AesManaged();
                tdes.Key = converted;
                tdes.Mode = CipherMode.ECB;
                tdes.Padding = PaddingMode.PKCS7;
                ICryptoTransform crypt = tdes.CreateEncryptor();
                byte[] plain = Encoding.UTF8.GetBytes(suuid);
                byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
                String enrmtKeyId = Convert.ToBase64String(cipher);

                enrmtKeyId = enrmtKeyId.Split('=')[0]; // Remove any trailing '='s
                enrmtKeyId = enrmtKeyId.Replace('+', '-'); // 62nd char of encoding
                enrmtKeyId = enrmtKeyId.Replace('/', '_'); // 63rd char of encoding

                dev.enrmtKeyId = enrmtKeyId;
                Console.WriteLine("dev.enrmtKeyId = " + dev.enrmtKeyId);
                dev.remoteCSEName = "csr-m2m_" + tbDeviceCTN.Text;
                label12.Text = dev.remoteCSEName;
                dev.nodeName = "nod-m2m_" + tbDeviceCTN.Text;
            }
        }

        public string DeviceHttpRequest(ReqHeader header, string data)
        {
            string resResult = string.Empty;

            try
            {
                wReq = (HttpWebRequest)WebRequest.Create(header.Url);
                wReq.Method = header.Method;
                if (header.Accept != string.Empty)
                    wReq.Accept = header.Accept;
                if (header.ContentType != string.Empty)
                    wReq.ContentType = header.ContentType;
                if (header.X_M2M_RI != string.Empty)
                    wReq.Headers.Add("X-M2M-RI", header.X_M2M_RI);
                if (header.X_M2M_Origin != string.Empty)
                    wReq.Headers.Add("X-M2M-Origin", header.X_M2M_Origin);
                if (header.X_M2M_NM != string.Empty)
                    wReq.Headers.Add("X-M2M-NM", header.X_M2M_NM);
                if (header.X_MEF_TK != string.Empty)
                    wReq.Headers.Add("X-MEF-TK", header.X_MEF_TK);
                if (header.X_MEF_EKI != string.Empty)
                    wReq.Headers.Add("X-MEF-EKI", header.X_MEF_EKI);
                if (header.X_OTA_CID != string.Empty)
                    wReq.Headers.Add("X-OTA-CID", header.X_OTA_CID);
                if (header.X_OTA_NT != string.Empty)
                    wReq.Headers.Add("X-OTA-NT", header.X_OTA_NT);
                wReq.Headers.Add("X-LGU-DM", tBoxDeviceModel.Text);
                wReq.Headers.Add("X-LGU-NI", "20");

                DevLogWrite(wReq.Method + " " + wReq.RequestUri, "T");
                Console.WriteLine(wReq.Method + " " + wReq.RequestUri + " HTTP/1.1");
                Console.WriteLine("");
                for (int i = 0; i < wReq.Headers.Count; ++i)
                    Console.WriteLine(wReq.Headers.Keys[i] + ": " + wReq.Headers[i]);
                Console.WriteLine("");

                if (data != string.Empty)
                {
                    DevLogWriteNoTime(data);

                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(data);
                    StringWriter writer = new StringWriter();
                    xDoc.Save(writer);
                    Console.WriteLine(writer.ToString());
                    Console.WriteLine("");

                    byte[] byteArray = Encoding.UTF8.GetBytes(data);
                    Stream dataStream = wReq.GetRequestStream();
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    dataStream.Close();
                }

                wReq.Timeout = 20000;          // 서버 응답을 20초동안 기다림
                using (wRes = (HttpWebResponse)wReq.GetResponse())
                {
                    httpResCode = (int)wRes.StatusCode;
                    httpRSC = string.Empty;
                    Console.WriteLine("HTTP/1.1 " + (int)wRes.StatusCode + " " + wRes.StatusCode.ToString());
                    Console.WriteLine("");
                    for (int i = 0; i < wRes.Headers.Count; ++i)
                    {
                        Console.WriteLine("[" + wRes.Headers.Keys[i] + "] " + wRes.Headers[i]);
                        if (wRes.Headers.Keys[i] == "x-m2m-rsc")
                            httpRSC = wRes.Headers[i];
                    }
                    Console.WriteLine("");
                    DevLogWrite((int)wRes.StatusCode + " " + wRes.StatusCode.ToString()+" (" + httpRSC + ")", "R");

                    Stream respPostStream = wRes.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);
                    resResult = readerPost.ReadToEnd();
                    if (resResult != string.Empty)
                    {
                        DevLogWriteNoTime(resResult);

                        if (resResult.StartsWith("<?xml"))
                        {
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.LoadXml(resResult);
                            StringWriter writer = new StringWriter();
                            xDoc.Save(writer);
                            Console.WriteLine(writer.ToString());
                        }
                        else if (resResult.StartsWith("{") || resResult.StartsWith("["))
                        {
                            string beautifiedJson = JValue.Parse(resResult).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                            Console.WriteLine(beautifiedJson);
                        }
                        else
                        {
                            if (resResult.Length > 256)
                                Console.WriteLine(resResult.Substring(0,256));
                            else
                                Console.WriteLine(resResult);
                        }
                        Console.WriteLine("");
                    }
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    httpResCode = (int)resp.StatusCode;
                    httpRSC = string.Empty;
                    Console.WriteLine("HTTP/1.1 " + (int)resp.StatusCode + " " + resp.StatusCode.ToString());
                    Console.WriteLine("");
                    for (int i = 0; i < resp.Headers.Count; ++i)
                    {
                        Console.WriteLine(" " + resp.Headers.Keys[i] + ": " + resp.Headers[i]);
                        if (resp.Headers.Keys[i] == "x-m2m-rsc")
                            httpRSC = resp.Headers[i];
                    }
                    Console.WriteLine("");
                    DevLogWrite((int)resp.StatusCode + " " + resp.StatusCode.ToString() + " (" + httpRSC + ")", "R");

                    Stream respPostStream = resp.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("UTF-8"), true);
                    string resError = readerPost.ReadToEnd();
                    if (resError != string.Empty)
                    {
                        DevLogWriteNoTime(resError);

                        if (resError.StartsWith("<?xml"))
                        {
                            XmlDocument xDoc = new XmlDocument();
                            xDoc.LoadXml(resError);
                            StringWriter writer = new StringWriter();
                            xDoc.Save(writer);
                            Console.WriteLine(writer.ToString());
                        }
                        else if (resError.StartsWith("{") || resError.StartsWith("["))
                        {
                            string beautifiedJson = JValue.Parse(resError).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                            Console.WriteLine(beautifiedJson);
                        }
                        else
                            Console.WriteLine(resError);
                        Console.WriteLine("");
                    }
                }
                else
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return resResult;
        }

        private void DevLogWrite(string data, string dir)
        {
            BeginInvoke(new Action(() =>
            {
                string time = DateTime.Now.ToString("hh:mm:ss");

                ListViewItem newitem = new ListViewItem(new string[] { time, dir, data });
                listView11.Items.Add(newitem);
                if (listView11.Items.Count > 35)
                    listView11.TopItem = listView11.Items[listView11.Items.Count - 1];
            }));
        }

        private void DevLogWriteNoTime(string data)
        {
            BeginInvoke(new Action(() =>
            {
                ListViewItem newitem = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, data });
                listView11.Items.Add(newitem);
                if (listView11.Items.Count > 35)
                    listView11.TopItem = listView11.Items[listView11.Items.Count - 1];
            }));
        }

        private void button45_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DevCSEBaseGet();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DevCSEBaseGet()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1";
            header.Method = "GET";
            header.Accept = "application/xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "CSEBase_Retrieve";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            string retStr = DeviceHttpRequest(header, string.Empty);
        }

        private void button46_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DevRemoteCSEGet();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DevRemoteCSEGet()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName;
            header.Method = "GET";
            header.Accept = "application/xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Retrieve";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            string retStr = DeviceHttpRequest(header, string.Empty);
        }

        private void button47_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
            {
                if (textBox3.Text != string.Empty)
                {
                    DevRemoteCSECreate();
                }
                else
                    MessageBox.Show("단말 IP를 확인하세요");
            }
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        // 3. RemoteCSE-Create
        private void DevRemoteCSECreate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1";
            header.Method = "POST";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=16";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Create";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = dev.remoteCSEName;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:csr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<cst>3</cst>";
            packetStr += "<csi>/" + dev.entityId + "</csi>";
            packetStr += "<cb>/" + dev.entityId + "/cb-1</cb>";
            packetStr += "<acpi>cb-1/" + dev.remoteCSEName + "/acp-m2m_" + tbDeviceCTN.Text + "</acpi>";
            packetStr += "<rr>true</rr>";
            packetStr += "<poa>http://" + textBox3.Text + ":" + "9901" + "</poa>";
            packetStr += "</m2m:csr>";
            string retStr = DeviceHttpRequest(header, packetStr);
            if (httpRSC == "2001")
                DevNodeCreate();
            else if (httpRSC == "4105")
                DevRemoteCSEUpdate();
        }

        private void DevNodeCreate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName;
            header.Method = "POST";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=14";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Node_Create";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = dev.nodeName;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:nod xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<ni>" + dev.remoteCSEName + "_nodid_" + tbDeviceCTN.Text + "</ni>";
            packetStr += "</m2m:nod>";
            string retStr = DeviceHttpRequest(header, packetStr);
            if (httpRSC == "2001" || httpRSC == "4105")
                ModemVerCreate();
        }

        private void ModemVerCreate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName;
            header.Method = "POST";
            header.Accept = "application/xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=13";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Mver_Create";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = "fwr-m2m_M" + tbDeviceCTN.Text;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:fwr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<mgd>1001</mgd>";
            packetStr += "<dc>module_firmware</dc>";
            packetStr += "<vr>"+ "1.0" + "</vr>";       // lbModemVer.Text
            packetStr += "<fwnnam></fwnnam>";
            packetStr += "<url></url>";
            packetStr += "<ud>false</ud>";
            packetStr += "<hwty>M</hwty>";
            packetStr += "</m2m:fwr>";
            string retStr = DeviceHttpRequest(header, packetStr);
            if (httpRSC == "2001" || httpRSC == "4105")
                DeviceVerCreate();
        }

        private void DeviceVerCreate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName;
            header.Method = "POST";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=13";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Dver_Create";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = "fwr-m2m_D" + tbDeviceCTN.Text;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:fwr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<mgd>1001</mgd>";
            packetStr += "<dc>module_firmware</dc>";
            packetStr += "<vr>" + tBoxDeviceVer.Text + "</vr>";
            packetStr += "<fwnnam></fwnnam>";
            packetStr += "<url></url>";
            packetStr += "<ud>false</ud>";
            packetStr += "<hwty>D</hwty>";
            packetStr += "</m2m:fwr>";
            string retStr = DeviceHttpRequest(header, packetStr);
            if (httpRSC == "2001" || httpRSC == "4105")
                RebootCreate();
        }

        private void RebootCreate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName;
            header.Method = "POST";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=13";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Reboot_Create";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = "rbo-m2m_" + tbDeviceCTN.Text;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:rbo xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<mgd>1009</mgd>";
            packetStr += "<dc>remote reboot</dc>";
            packetStr += "<rbo>false</rbo>";
            packetStr += "</m2m:rbo>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button49_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
            {
                if (textBox3.Text != string.Empty)
                {
                    DevRemoteCSEUpdate();
                }
                else
                    MessageBox.Show("단말 IP를 확인하세요");
            }
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DevRemoteCSEUpdate()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/"+ dev.remoteCSEName;
            header.Method = "PUT";
            header.Accept = "application/xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Update";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:csr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<cst>3</cst>";
            packetStr += "<csi>/" + dev.entityId + "</csi>";
            packetStr += "<cb>/" + dev.entityId + "/cb-1</cb>";
            packetStr += "<acpi>cb-1/" + dev.remoteCSEName + "/acp-m2m_" + tbDeviceCTN.Text + "</acpi>";
            packetStr += "<rr>true</rr>";
            packetStr += "<poa>http://" + textBox3.Text + ":" + "9901" + "</poa>";
            packetStr += "</m2m:csr>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button48_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DevRemoteCSEDEL();
            else
                MessageBox.Show("서버인증파라미터 세팅하세요");
        }

        // 3. RemoteCSE-Delete
        private void DevRemoteCSEDEL()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName + "/rbo-m2m_" + tbDeviceCTN.Text;
            header.Method = "DELETE";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Cver_Delete";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            string retStr = DeviceHttpRequest(header, string.Empty);

            header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName + "/fwr-m2m_D" + tbDeviceCTN.Text;
            header.Method = "DELETE";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Mver_Delete";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            retStr = DeviceHttpRequest(header, string.Empty);

            header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName + "/fwr-m2m_M" + tbDeviceCTN.Text;
            header.Method = "DELETE";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Mver_Delete";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            retStr = DeviceHttpRequest(header, string.Empty);

            header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName;
            header.Method = "DELETE";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Node_Delete";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            retStr = DeviceHttpRequest(header, string.Empty);

            header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName;
            header.Method = "DELETE";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "RemoteCSE_Delete";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;
            retStr = DeviceHttpRequest(header, string.Empty);
        }

        private void listView11_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemColl = listView11.SelectedItems;
            foreach (ListViewItem item in itemColl)
            {
                string data = item.SubItems[3].Text;
                if (data.StartsWith("<?xml"))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.LoadXml(data);
                    StringWriter writer = new StringWriter();
                    xDoc.Save(writer);
                    MessageBox.Show(writer.ToString(), "상세 내용");
                }
                else if (data.StartsWith("{") || data.StartsWith("["))
                {
                    string beautifiedJson = JValue.Parse(data).ToString((Newtonsoft.Json.Formatting)Formatting.Indented);
                    MessageBox.Show(beautifiedJson, "상세 내용");
                }
            }
        }

        private void button75_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
            {
                if (svr.entityId != string.Empty)
                {
                    string txData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " oneM2M module";
                    ForwardOneM2MData(svr.entityId, "TEST", txData);
                }
                else
                    MessageBox.Show("서버인증파라미터가 없습니다.");
            }
            else
                MessageBox.Show("단말인증파라미터가 없습니다.");
        }

        private void ForwardOneM2MData(string entityID, string folder, string data)
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/" + entityID + "/" + folder;
            header.Method = "POST";
            header.X_M2M_Origin = dev.entityId;
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "data_send";
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.Accept = "application/xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=4";
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:cin xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<cnf>application/xml</cnf>";
            packetStr += "<con>" + data + "</con>";
            label1.Text = data;
            packetStr += "</m2m:cin>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button55_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DeviceVerReport();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }


        private void DeviceVerReport()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName + "/" + "fwr-m2m_D" + tbDeviceCTN.Text;
            header.Method = "PUT";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Dver_Update";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:fwr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<mgd>1001</mgd>";
            packetStr += "<dc>module_firmware</dc>";
            packetStr += "<vr>" + tBoxDeviceVer.Text + "</vr>";
            packetStr += "<fwnnam></fwnnam>";
            packetStr += "<url></url>";
            packetStr += "<uds><sus>1</sus></uds>";
            packetStr += "</m2m:fwr>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button58_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                ModemVerReport();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void ModemVerReport()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName + "/" + "fwr-m2m_M" + tbDeviceCTN.Text;
            header.Method = "PUT";
            header.Accept = "application/xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Mver_Update";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:fwr xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<mgd>1001</mgd>";
            packetStr += "<dc>module_firmware</dc>";
            packetStr += "<vr>" + "1.0" + "</vr>";      // lbModemVer.Text
            packetStr += "<fwnnam></fwnnam>";
            packetStr += "<url></url>";
            packetStr += "<uds><sus>1</sus></uds>";
            packetStr += "</m2m:fwr>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button57_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DeviceVerCheck();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DeviceVerCheck()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MFOTAIP + ":" + oneM2MFOTAPort + "/ota/updateVersionCheck/firmware/" + tbSvcCd.Text + "/" + tBoxDeviceModel.Text + "/" + tBoxDeviceVer.Text;
            header.Method = "GET";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Dver_Check";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = textBox6.Text;
            header.X_OTA_NT = (comboBox5.SelectedIndex+1).ToString();

            string retStr = DeviceHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {
                string version = string.Empty;
                string filename = string.Empty;
                string url = string.Empty;
                string dc = string.Empty;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(retStr);

                XmlNodeList xnList = xDoc.SelectNodes("/fwr"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    version = xn["vr"].InnerText;
                    filename = xn["fwnnam"].InnerText;
                    url = xn["url"].InnerText;
                    dc = xn["dc"].InnerText;
                }
                lbdevicever.Text = version;

                DeviceVerDownload(url, filename);
            }
        }

        private void DeviceVerDownload(string url, string filename)
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MFOTAIP + ":" + oneM2MFOTAPort + "/ota/firmware/" + url + "/" + filename;
            header.Method = "GET";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Dver_Download";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = textBox6.Text;
            header.X_OTA_NT = (comboBox5.SelectedIndex + 1).ToString();

            string retStr = DeviceHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {

            }
        }

        private void button59_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                ModemVerCheck();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void ModemVerCheck()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MFOTAIP + ":" + oneM2MFOTAPort + "/ota/updateVersionCheck/moduleFirmware/" + tbSvcCd.Text + "/" + tBoxDeviceModel.Text + "/" + "1.0";     // lbModemVer.Text;
            header.Method = "GET";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Mver_Check";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = textBox6.Text;
            header.X_OTA_NT = (comboBox5.SelectedIndex + 1).ToString();

            string retStr = DeviceHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {
                string version = string.Empty;
                string filename = string.Empty;
                string url = string.Empty;
                string dc = string.Empty;
                XmlDocument xDoc = new XmlDocument();
                xDoc.LoadXml(retStr);

                XmlNodeList xnList = xDoc.SelectNodes("/fwr"); //접근할 노드
                foreach (XmlNode xn in xnList)
                {
                    version = xn["vr"].InnerText;
                    filename = xn["fwnnam"].InnerText;
                    url = xn["url"].InnerText;
                    dc = xn["dc"].InnerText;
                }
                lbdevicever.Text = version;

                ModemVerDownload(url, filename);
            }
        }

        private void ModemVerDownload(string url, string filename)
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MFOTAIP + ":" + oneM2MFOTAPort + "/ota/firmware/" + url + "/" + filename;
            header.Method = "GET";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Mver_Download";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = textBox6.Text;
            header.X_OTA_NT = (comboBox5.SelectedIndex + 1).ToString();

            string retStr = DeviceHttpRequest(header, string.Empty);
            if (retStr != string.Empty)
            {

            }
        }

        private void button60_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DevAcpCreate("63","*");
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DevAcpCreate(string mode, string owner)
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName;
            header.Method = "POST";
            header.Accept = "application/xml";
            header.ContentType = "application/vnd.onem2m-res+xml;ty=1";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Acp_Create";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = "acp-m2m_" + tbDeviceCTN.Text;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:acp xmlns:m2m=\"http://www.onem2m.org/xml/protocols\">";
            packetStr += "<pv><acr><acor>" + owner+"</acor>";
            packetStr += "<acop>"+mode+"</acop></acr></pv>";
            packetStr += "<pvs><acr><acor>"+dev.entityId+"</acor>";
            packetStr += "<acop>63</acop></acr></pvs>";
            packetStr += "</m2m:acp>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button64_Click(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DevAcpGet();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DevAcpGet()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName +"/"+ "acp-m2m_" + tbDeviceCTN.Text;
            header.Method = "GET";
            header.Accept = "application/xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Acp_Get";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string retStr = DeviceHttpRequest(header, string.Empty);
        }

        private void DevAcpUpdate(string mode, string owner)
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + "acp-m2m_" + tbDeviceCTN.Text;
            header.Method = "PUT";
            header.Accept = "application/xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Acp_Update";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string packetStr = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            packetStr += "<m2m:acp xmlns:m2m=\"http://www.onem2m.org/xml/protocols\"><pv>";
            packetStr += "<acr><acor>" + owner + "</acor>";
            packetStr += "<acop>" + mode + "</acop></acr></pv>";
            packetStr += "<pvs><acr><acor>" + dev.entityId + "</acor>";
            packetStr += "<acop>63</acop></acr></pvs>";
            packetStr += "</m2m:acp>";
            string retStr = DeviceHttpRequest(header, packetStr);
        }

        private void button61_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                DevAcpDelete();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }

        private void DevAcpDelete()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + "acp-m2m_" + tbDeviceCTN.Text;
            header.Method = "DELETE";
            header.Accept = "application/xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Acp_Delete";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
            header.X_OTA_CID = string.Empty;
            header.X_OTA_NT = string.Empty;

            string retStr = DeviceHttpRequest(header, string.Empty);
        }

        private void button37_Click_1(object sender, EventArgs e)
        {
        }

        /***** 아래부터는 Http Server 관련 *****/
        HttpListener listener;
        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("----------서비스 서버 설정----------");
            if (svrState == "STOP")
            {
                StartHttpServer();
                svrState = "RUN";
                btn.Text = "서버 동작중(중지)";
            }
            else
            {
                StopHttpServer();
                svrState = "STOP";
                btn.Text = "서버 시작";
            }
        }

        // This example requires the System and System.Net namespaces.
        public static void SimpleListenerExample(string[] prefixes)
        {
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("Windows XP SP2 or Server 2003 is required to use the HttpListener class.");
                return;
            }
            // URI prefixes are required,
            // for example "http://contoso.com:8080/index/".
            if (prefixes == null || prefixes.Length == 0)
                throw new ArgumentException("prefixes");

            // Create a listener.
            HttpListener listener = new HttpListener();
            // Add the prefixes.
            foreach (string s in prefixes)
            {
                listener.Prefixes.Add(s);
            }
            listener.Start();
            Console.WriteLine("Listening...");
            // Note: The GetContext method blocks while waiting for a request.
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            // Obtain a response object.
            HttpListenerResponse response = context.Response;
            // Construct a response.
            string responseString = "<HTML><BODY> Hello world!</BODY></HTML>";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
            // Get a response stream and write the response to it.
            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            // You must close the output stream.
            output.Close();
            listener.Stop();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (svrState != "STOP")
            {
                StopHttpServer();
            }
        }
        private void StartHttpServer()
        {
            listener = new HttpListener();
            listener.Prefixes.Add("http://+:8000/");
            listener.Start();
            listener.BeginGetContext(this.OnRequested, this.listener);
            Console.WriteLine("StartHttpServer");
        }

        private void StopHttpServer()
        {
            this.listener.Close();
        }

        private void OnRequested(IAsyncResult result)
        {
            HttpListener listener = (HttpListener)result.AsyncState;
            if (!listener.IsListening)
            {
                Console.WriteLine("listening finished.");
                return;
            }

            Console.WriteLine("OnRequested start.");
            Console.WriteLine("OnRequested result.IsCompleted " + result.IsCompleted);
            Console.WriteLine("OnRequested result.CompletedSynchronously " + result.CompletedSynchronously);
            Console.WriteLine("OnRequested listener.IsListening " + listener.IsListening);

            HttpListenerContext ctx = listener.EndGetContext(result);
            HttpListenerRequest req = null;
            HttpListenerResponse res = null;
            StreamReader reader = null;
            StreamWriter writer = null;

            try
            {
                req = ctx.Request;
                res = ctx.Response;

                DisplayWebHeaderCollection(req);

                reader = new StreamReader(req.InputStream);
                writer = new StreamWriter(res.OutputStream);

                string received = reader.ReadToEnd();
                if (received != string.Empty)
                {
                    Console.WriteLine("[ 수신 데이터 ]");
                    Console.WriteLine(received);
                    ParsingJson(received, req.Url.AbsolutePath);
                }
                //writer.Write(received);
                //writer.Flush();       

                //res.StatusCode = (int)HttpStatusCode.NotFound;
                res.Headers.Add("X-M2M-RI", "response_1");
                res.Headers.Add("X-M2M-RSC", "2000");
                res.StatusCode = (int)HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                try
                {
                    if (null != writer) writer.Close();
                    if (null != reader) reader.Close();
                    if (null != res) res.Close();  // close할 때 응답이 완료됨..
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            listener.BeginGetContext(this.OnRequested, listener);
        }
        public void DisplayWebHeaderCollection(HttpListenerRequest request)
        {
            Console.WriteLine("[ request.Url.AbsolutePath ]");
            Console.WriteLine("   " + request.Url.AbsolutePath);

            System.Collections.Specialized.NameValueCollection headers = request.Headers;

            foreach (string key in headers.AllKeys)
            {
                string[] values = headers.GetValues(key);
                if (values.Length > 0)
                {
                    Console.WriteLine("[ " + key + " ]");
                    foreach (string value in values)
                    {
                        Console.WriteLine("   " + value);
                    }
                }
                else
                    Console.WriteLine("There is no value associated with the header.");
            }
        }

        private void ParsingJson(string jsonStr, string path)
        {
            try
            {
                JObject obj = JObject.Parse(jsonStr);
                if (path == "/" + svr.entityId + "/10250/0/0") // 데이터 수신
                {
                    string temp = obj["nev"]["rep"]["m2m:cin"]["con"].ToString();
                    string data = Encoding.UTF8.GetString(Convert.FromBase64String(temp));
                    string deviceEntityId = obj["cr"].ToString();
                    if (data != string.Empty)
                        Console.WriteLine("[" + deviceEntityId + "][데이터 수신]" + data);
                }
                else if (path == "/" + svr.entityId + "/bs") // 부트스트랩
                {
                    string deviceEntityId = obj["cr"].ToString();
                    Console.WriteLine("[" + deviceEntityId + "] Bootstrap 요청 수신");
                }
                else if (path == "/" + svr.entityId + "/rd") // 레지스터
                {
                    string deviceEntityId = obj["cr"].ToString();
                    Console.WriteLine("[" + deviceEntityId + "] Registration 요청 수신");
                }
                else
                {
                    Console.WriteLine("[ParsingJson] path = " + path);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private void btnUDPServer_Click(object sender, EventArgs e)
        {
            // Bind()
            IPEndPoint ipep = new IPEndPoint(IPAddress.Any, 5555);
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            server.Bind(ipep);

            Console.WriteLine("UDP Server Start");

            IPEndPoint udpsender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(udpsender);

            byte[] _data = new byte[1024];

            // ReceiveFrom()
            server.ReceiveFrom(_data, ref remote);
            Console.WriteLine("{0} : \r\nServar Recieve Data : {1}", remote.ToString(),
                Encoding.Default.GetString(_data));

            // string --> byte[]
            _data = Encoding.Default.GetBytes("Client SendTo Data");

            // SendTo()
            server.SendTo(_data, _data.Length, SocketFlags.None, remote);

            // Close()
            server.Close();

            Console.WriteLine("UDP Server Stop");
        }

        private void btnUDPClient_Click(object sender, EventArgs e)
        {
            IPEndPoint ipep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5555);
            Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            // string --> byte[]
            byte[] _data = Encoding.Default.GetBytes("Server SendTo Data");

            // Connect() 후 Send() 가능

            // SendTo()
            client.SendTo(_data, _data.Length, SocketFlags.None, ipep);

            IPEndPoint udpsender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(udpsender);

            _data = new byte[1024];

            // ReceiveFrom()
            client.ReceiveFrom(_data, ref remote);
            Console.WriteLine("{0} : \r\nClient Receive Data : {1}", remote.ToString(),
                Encoding.Default.GetString(_data));

            // Close()
            client.Close();
        }
    }

    public class Device
    {
        public string imsi { get; set; }            // 디바이스 전화번호
        public string imei { get; set; }            // 디바이스 IMEI
        public string iccid { get; set; }           // 디바이스 ICCID
        public string entityId { get; set; }        // oneM2M 디바이스 EntityID
        public string uuid { get; set; }            // oneM2M 디바이스 UUID

        public string maker { get; set; }           // 모듈 제조사
        public string model { get; set; }           // 모듈 모델명
        public string version { get; set; }         // 모듈 펌웨어 버전

        public string type { get; set; }            // 플랫폼 연동 방식 (None/oneM2M/LwM2M)

        public string enrmtKey { get; set; }        // oneM2M 인증 KeyID를 생성하기 위한 Key
        public string token { get; set; }           // 인증구간 통신을 위해 발급하는 Token
        public string enrmtKeyId { get; set; }      // MEF 인증 결과를 통해 생성하는 ID
        public string remoteCSEName { get; set; }   // RemoteCSE 리소스 이름
        public string nodeName { get; set; }        // Node 리소스 이름
    }

    public class ServiceServer
    {
        public string svcSvrCd { get; set; }        // 서비스 서버의 시퀀스
        public string svcCd { get; set; }           // 서비스 서버의 서비스코드
        public string svcSvrNum { get; set; }       // 서비스 서버의 Num ber

        public string enrmtKey { get; set; }        // oneM2M 인증 KeyID를 생성하기 위한 Key
        public string entityId { get; set; }        // oneM2M에서 사용하는 서버 ID
        public string token { get; set; }           // 인증구간 통신을 위해 발급하는 Token

        public string enrmtKeyId { get; set; }      // MEF 인증 결과를 통해 생성하는 ID

        public string remoteCSEName { get; set; }   // RemoteCSE 리소스 이름
    }

    public class ReqHeader
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public string Accept { get; set; }
        public string ContentType { get; set; }
        public string X_M2M_RI { get; set; } // Request ID(임의 값)
        public string X_M2M_Origin { get; set; } // 서비스서버의 Entity ID
        public string X_MEF_TK { get; set; } // Password : MEF 인증으로 받은 Token 값
        public string X_MEF_EKI { get; set; } // Username(EKI) : MEF 인증으로 받은 Enrollment Key 로 생성한 Enrollment Key ID
        public string X_M2M_NM { get; set; } // 리소스 이름
        public string X_OTA_CID { get; set; } // Network Cell ID
        public string X_OTA_NT { get; set; } // Network Type
    }

}
