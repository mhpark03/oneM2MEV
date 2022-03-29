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
            string[] ports = SerialPort.GetPortNames();
            if(ports.Length == 0)
            {
                logPrintInTextBox("연결 가능한 COM PORT가 없습니다.", "");
            }
            else
            {
                cBoxCOMPORT.Items.Clear();
                cBoxCOMPORT.Items.AddRange(ports);
                cBoxCOMPORT.SelectedIndex = 0;
            }

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

            lbActionState.Text = "idle";

            listView3.View = View.Details;
            listView3.GridLines = true;
            listView3.FullRowSelect = true;
            listView3.CheckBoxes = false;

            listView3.Columns.Add("시간", 60, HorizontalAlignment.Center);
            listView3.Columns.Add("state", 120, HorizontalAlignment.Left);
            listView3.Columns.Add("  ", 30, HorizontalAlignment.Center);
            listView3.Columns.Add("  AT COMMAND", 1000, HorizontalAlignment.Left);

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

        private void doOpenComPort()
        {
            if (!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.PortName = cBoxCOMPORT.Text;
                    serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                    serialPort1.DataBits = (int)8;
                    serialPort1.StopBits = StopBits.One;
                    serialPort1.Parity = Parity.None;
                    if (cbDTR.Checked == true)
                        serialPort1.DtrEnable = true;
                    else
                        serialPort1.DtrEnable = false;
                    if (cbRTS.Checked == true)
                        serialPort1.RtsEnable = true;
                    else
                        serialPort1.RtsEnable = false;
                    serialPort1.ReadTimeout = (int)500;
                    serialPort1.WriteTimeout = (int)500;

                    serialPort1.Open();

                    if (lbActionState.Text == states.closed.ToString())
                        lbActionState.Text = states.idle.ToString();
                    
                    if (dev.entityId == string.Empty)
                        progressBar1.Value = 50;
                    else
                        progressBar1.Value = 100;
                    timer1.Start();
                    logPrintInTextBox("COM PORT가 연결 되었습니다.", "");
                }
                catch (Exception err)
                {
                    logPrintInTextBox(err.Message, "");
                }
            }

        }

        private void doCloseComPort()
        {
            progressBar1.Value = 0;
            serialPort1.Close();
            if (lbActionState.Text == states.idle.ToString())
                lbActionState.Text = states.closed.ToString();
            timer1.Stop();
            logPrintInTextBox("COM PORT가 해제 되었습니다.","");

        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                this.doCloseComPort();
            }
            else
            {
                this.doOpenComPort();
            }

        }

        private void sendDataOut(string dataOUT)
        {
            try
            {
                if (serialPort1.IsOpen)
                {
                    string sendmsg = dataOUT;
                    sendmsg = dataOUT + "\r\n";

                    serialPort1.Write(sendmsg);
                    logPrintInTextBox(sendmsg, "tx");

                    nextcommand = string.Empty;
                    nextresponse = string.Empty;
                }
                else
                {
                    progressBar1.Value = 0;
                    MessageBox.Show("COM 포트가 오픈되어 있지 않습니다.");
                    this.doOpenComPort();     // Serial port가 끊어진 것으로 판단, 포트 재오픈
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Value = 0;
            }
        }

        // 송수신 명령/응답 값과 동작 설명을 textbox에 삽입하고 앱 종료시 로그파일로 저장한다.
        public void logPrintInTextBox(string message, string kind)
        {
            if (message != "\r" && message != "\r\r")
            {
                if (kind == "tx")
                    kind = "T";
                else if (kind == "rx")
                {
                    kind = "R";

                    /* Debug를 위해 Hex로 문자열 표시*/
/*
                    char[] charValues = message.ToCharArray();
                    string hexOutput = "";
                    foreach (char _eachChar in charValues)
                    {
                        int value = Convert.ToInt32(_eachChar);
                        if (value < 16)
                            hexOutput += "0";
                        hexOutput += String.Format("{0:X}", value);
                    }
                    message = hexOutput;
*/                  
                }
                else
                    kind = " ";

                SetTextBox(listView3, kind + message);
            }
        }

        private void SetTextBox(Control ctr, string message)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (ctr.InvokeRequired)
            {
                Ctr_Involk CI = new Ctr_Involk(SetTextBox);
                ctr.Invoke(CI, ctr, message);
            }
            else
            {
                string kind = message.Substring(0, 1);
                string msg = message.Substring(1, message.Length - 1);
                string time = DateTime.Now.ToString("hh:mm:ss");

                ListViewItem newitem3;
                ListViewItem newitem4;
                ListViewItem newitem5;
                if (kind == " ")
                {
                    newitem3 = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, msg });
                    newitem4 = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, msg });
                    newitem5 = new ListViewItem(new string[] { string.Empty, string.Empty, string.Empty, msg });
                }
                else
                {
                    newitem3 = new ListViewItem(new string[] { time, lbActionState.Text, kind, msg });
                    newitem4 = new ListViewItem(new string[] { time, lbActionState.Text, kind, msg });
                    newitem5 = new ListViewItem(new string[] { time, lbActionState.Text, kind, msg });
                }
                listView3.Items.Add(newitem3);
            }
        }

        // serial port에서 data 수신이 될 때, 발생하는 이벤트 함수
        private void SerialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                SerialPort rcvPort = (SerialPort)sender;
                dataIN += rcvPort.ReadExisting();               // 수신한 버퍼에 있는 데이터 모두 받음
                this.Invoke(new EventHandler(ShowData));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 수신 데이터 처리 thread 시작
        private void ShowData(object sender, EventArgs e)
        {
            char[] charValues = dataIN.ToCharArray();

            /* Debug를 위해 Hex로 문자열 표시*/
            /*
            string hexOutput = "";
            foreach (char _eachChar in charValues)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(_eachChar);
                // Convert the decimal value to a hexadecimal value in string form.
                if (value < 16)
                    hexOutput += "0";
                hexOutput += String.Format("{0:X}", value);
            }
            logPrintInTextBox(hexOutput, "");
            */

            if (charValues.Length >= 2)
            {
                if (charValues[charValues.Length - 1] == '\n' || charValues[charValues.Length - 2] == '\n' || charValues[charValues.Length - 2] == '>')
                {
                    string[] words = dataIN.Split('\n');    // 수신한 데이터를 한 문장씩 나누어 array에 저장

                    foreach (var word in words)
                    {
                        string str1;

                        int lflength = word.IndexOf("\r");
                        if (lflength > 1)
                        {
                            str1 = word.Substring(0, lflength);    // \r\n를 제외하고 명령어만 처리하기 위함
                        }
                        else
                        {
                            str1 = word;
                        }

                        if (str1 != "")             // 빈 줄은 제외하기 위함
                        {
                            this.parseRXData(str1);
                        }
                    }

                    if (charValues[charValues.Length - 1] != '\n')
                    {
                        dataIN = charValues[charValues.Length - 1].ToString();    // \r\n를 제외하고 나머지 한글자 저장
                    }
                    else
                    {
                        dataIN = "";
                    }
                }
            }
        }

        // 수신한 데이터 한 줄에 대해 후처리가 필요한 응답 값을 찾아서 설정함 
        private void parseRXData(string rxMsg)
        {
            logPrintInTextBox(rxMsg,"rx");          // 수신한 데이터 한줄을 표시

            if ((rxMsg != "\r") && (rxMsg != "\n"))
            {
                if (rxMsg == "OK")
                {
                    if (lbActionState.Text == states.testatcmd.ToString())
                    {
                        MessageBox.Show("OK 응답을 받았습니다.");
                        lbActionState.Text = states.idle.ToString();
                    }
                    else
                    {
                        OKReceived();
                    }
                }
                else if (rxMsg == "ERROR")
                {
                    if (lbActionState.Text == states.testatcmd.ToString())
                    {
                        MessageBox.Show("ERROR 응답을 받았습니다.");
                        lbActionState.Text = states.idle.ToString();
                        nextcommand = "";
                    }
                    else if (lbActionState.Text == states.modemFWUPboot.ToString())
                    {
                        // 디바이스 펌웨어 버전 등록을 위해 플랫폼 서버 MEF AUTH 요청
                        this.sendDataOut(commands["setmefauth"] + tbSvcCd.Text + "," + tBoxDeviceModel.Text + "," + tBoxDeviceVer.Text + "," + tBoxDeviceSN.Text);
                        lbActionState.Text = states.mfotamefauth.ToString();
                        nextresponse = "$OM_AUTH_RSP=";
                    }
                    else if (lbActionState.Text == states.onem2mtc0211040.ToString() || lbActionState.Text == states.onem2mtc0211041.ToString())
                    {
                        // 디바이스 펌웨어 버전 등록을 위해 플랫폼 서버 MEF AUTH 요청
                        this.sendDataOut(commands["setmefauth"] + tbSvcCd.Text + "," + tBoxDeviceModel.Text + "," + tBoxDeviceVer.Text + "," + tBoxDeviceSN.Text);
                        lbActionState.Text = states.onem2mtc0211042.ToString();
                        nextresponse = "$OM_AUTH_RSP=";
                    }
                    /*
                    else if (lbActionState.Text == states.onem2mtc0201022.ToString())
                    {
                        this.sendDataOut(commands["getonem2mmode"]);
                        lbActionState.Text = states.onem2mtc0201023.ToString();
                        nextresponse = "$LGTMPF=";
                    }
                    */
                    else if (lbActionState.Text == states.resetmodechk.ToString())
                    {
                        this.sendDataOut(commands["getonem2mmode"]);
                        lbActionState.Text = states.resetmodechk.ToString();
                        nextresponse = "$LGTMPF=";
                    }
                    else
                    {
                        lbActionState.Text = states.idle.ToString();
                        nextcommand = string.Empty;
                        nextresponse = string.Empty;
                    }
                }
                else if (rxMsg.StartsWith("$OM_N_INS_RSP=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    // oneM2M subscription 설정에 의한 data 수신 이벤트

                        // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                        string cmd = "$OM_N_INS_RSP=";
                        int first = rxMsg.IndexOf(cmd) + cmd.Length;
                        string str2 = rxMsg.Substring(first, rxMsg.Length - first);
                        string[] rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                        int rxdatasize = Convert.ToInt32(rcvdatas[1]);
                        if (rxdatasize == rcvdatas[2].Length)
                            logPrintInTextBox(rcvdatas[0] + "폴더에 " + rcvdatas[2] + "를 수신하였습니다.", "");
                        else
                            MessageBox.Show("수신한 데이터 사이즈를 확인하세요", "");

                        if (lbActionState.Text == states.onem2mtc0206032.ToString() || lbActionState.Text == states.onem2mtc0206031.ToString())
                        {
                            this.sendDataOut(commands["setrcvmanu"]);
                            lbActionState.Text = states.onem2mtc0206042.ToString();
                        }
                        else
                        {
                            lbActionState.Text = states.idle.ToString();
                            nextcommand = string.Empty;
                        }
                }
                else if (rxMsg.StartsWith("$OM_NOTI_IND=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    // oneM2M subscription 설정에 의한 data 수신 이벤트

                    // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                    string cmd = "$OM_NOTI_IND=";
                    int first = rxMsg.IndexOf(cmd) + cmd.Length;
                    string str2 = rxMsg.Substring(first, rxMsg.Length - first);

                    if (lbActionState.Text == states.onem2mtc0205043.ToString() 
                        || lbActionState.Text == states.onem2mtc0206011.ToString()
                        || lbActionState.Text == states.onem2mtc0206012.ToString())
                    {
                        this.sendDataOut(commands["getonem2mdata"] + str2);
                        if (lbActionState.Text == states.onem2mtc0206012.ToString())
                            lbActionState.Text = states.onem2mtc0207012.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0207011.ToString();
                        nextresponse = "$OM_R_INS_RSP=";
                    }
                    else
                    {
                        this.sendDataOut(commands["getonem2mdata"] + str2);
                        if (lbActionState.Text == states.sendonedevdb.ToString())
                            lbActionState.Text = states.sendonedevdb2.ToString();
                        else
                            lbActionState.Text = states.getonem2mdata.ToString();
                        nextresponse = "$OM_R_INS_RSP=";
                    }
                }
                else if (rxMsg.StartsWith("$OM_DEV_FWDL_FAIL=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    lbActionState.Text = states.idle.ToString();
                    nextcommand = string.Empty;
                    nextresponse = string.Empty;
                }
                else if (rxMsg.StartsWith("$OM_DEV_FWUP_RSP=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                    string cmd = "$OM_DEV_FWUP_RSP =";
                    int first = rxMsg.IndexOf(cmd) + cmd.Length;
                    string str2 = rxMsg.Substring(first, rxMsg.Length - first);

                    string[] deviceverinfos = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                    if (lbActionState.Text == states.idle.ToString())
                    {

                        tBoxDeviceVer.Text = deviceverinfos[1];
                        logPrintInTextBox("수신한 DEVICE의 버전은 " + deviceverinfos[1] + "입니다. 업데이트를 시도합니다.", "");
                        this.sendDataOut(commands["deviceFWUPstart"]);
                        if (dev.model == "EC25" || dev.model == "EC21")               //쿼텔/oneM2M 모듈
                            lbActionState.Text = states.deviceFWUPstart.ToString();
                        else
                            lbActionState.Text = states.deviceFWDownload.ToString();
                        nextresponse = "$OM_DEV_FWDL_START=";

                        oneM2Mrcvsize = 0;
                        oneM2Mtotalsize = 0;
                    }
                    else
                    {
                        if (deviceverinfos[0] == "2000")
                        {

                            tBoxDeviceVer.Text = deviceverinfos[1];
                            logPrintInTextBox("수신한 DEVICE의 버전은 " + deviceverinfos[1] + "입니다. 업데이트를 시도합니다.", "");

                            this.sendDataOut(commands["deviceFWUPstart"]);
                            nextresponse = "$OM_DEV_FWDL_START=";

                            oneM2Mrcvsize = 0;
                            oneM2Mtotalsize = 0;
                            if (lbActionState.Text == states.getdeviceSvrVer.ToString())
                            {
                                if (dev.model == "EC25" || dev.model == "EC21")               //쿼텔/oneM2M 모듈
                                    lbActionState.Text = states.deviceFWUPstart.ToString();
                                else
                                    lbActionState.Text = states.deviceFWDownload.ToString();
                            }
                            else
                            {
                                if (dev.model == "EC25" || dev.model == "EC21")               //쿼텔/oneM2M 모듈
                                    lbActionState.Text = states.onem2mtc0210034.ToString();
                                else
                                    lbActionState.Text = states.onem2mtc0210031.ToString();
                            }
                        }
                        else if (deviceverinfos[0] == "9001")
                        {

                            if (lbActionState.Text == states.getdeviceSvrVer.ToString())
                            {
                                MessageBox.Show("현재 DEVICE 버전이 최신버전입니다.", "");
                                lbActionState.Text = states.idle.ToString();
                            }
                            else
                            {
                                this.sendDataOut(commands["getmodemSvrVer"]);
                                lbActionState.Text = states.onem2mtc021101.ToString();
                                nextresponse = "$OM_MODEM_FWUP_RSP=";
                            }
                        }
                        else
                        {

                            if (lbActionState.Text == states.getdeviceSvrVer.ToString())
                                lbActionState.Text = states.idle.ToString();
                            else
                            {
                                this.sendDataOut(commands["getmodemSvrVer"]);
                                lbActionState.Text = states.onem2mtc021101.ToString();
                                nextresponse = "$OM_MODEM_FWUP_RSP=";
                            }
                        }
                    }
                }
                else if (rxMsg.StartsWith("$OM_MODEM_FWUP_RSP=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                    string cmd = "$OM_MODEM_FWUP_RSP=";
                    int first = rxMsg.IndexOf(cmd) + cmd.Length;
                    string str2 = rxMsg.Substring(first, rxMsg.Length - first);

                    string[] modemverinfos = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                    if (lbActionState.Text == states.idle.ToString())
                    {

                        logPrintInTextBox("수신한 MODEM의 버전은 " + modemverinfos[1] + "입니다. 업데이트를 시도합니다.", "");

                        this.sendDataOut(commands["modemFWUPstart"]);
                        lbActionState.Text = states.modemFWUPstart.ToString();
                        nextresponse = "$OM_MODEM_FWDL_FINISH";
                    }
                    else
                    {
                        if (modemverinfos[0] == "2000")
                        {
                            logPrintInTextBox("수신한 MODEM의 버전은 " + modemverinfos[1] + "입니다. 업데이트를 시도합니다.", "");

                            this.sendDataOut(commands["modemFWUPstart"]);
                            if (lbActionState.Text == states.getmodemSvrVer.ToString())
                                lbActionState.Text = states.modemFWUPstart.ToString();
                            else
                                lbActionState.Text = states.onem2mtc0211031.ToString();
                            nextresponse = "$OM_MODEM_FWDL_FINISH";
                        }
                        else if (modemverinfos[0] == "9001")
                        {
                            if (lbActionState.Text == states.getmodemSvrVer.ToString())
                            {
                                MessageBox.Show("현재 MODEM 버전이 최신버전입니다.", "");
                                lbActionState.Text = states.idle.ToString();
                            }
                            else
                            {
                                this.sendDataOut(commands["delsubscript"] + "StoD");
                                lbActionState.Text = states.onem2mtc0212022.ToString();
                                nextresponse = "$OM_D_SUB_RSP=";
                            }
                        }
                        else
                        {
                            if (lbActionState.Text == states.getmodemSvrVer.ToString())
                                lbActionState.Text = states.idle.ToString();
                            else
                            {
                                this.sendDataOut(commands["delsubscript"] + "StoD");
                                lbActionState.Text = states.onem2mtc0212022.ToString();
                                nextresponse = "$OM_D_SUB_RSP=";
                            }
                        }
                    }
                }
                else if (rxMsg.StartsWith("$OM_POA_NOTI=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                    string cmd = "$OM_POA_NOTI=";
                    int first = rxMsg.IndexOf(cmd) + cmd.Length;
                    string str2 = rxMsg.Substring(first, rxMsg.Length - first);
                }
                else if (rxMsg.StartsWith("$OM_S_RCIN_RSP=", System.StringComparison.CurrentCultureIgnoreCase))
                {
                    // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                    string cmd = "$OM_S_RCIN_RSP=";
                    int first = rxMsg.IndexOf(cmd) + cmd.Length;
                    string str2 = rxMsg.Substring(first, rxMsg.Length - first);

                    // 플랫폼 서버에 device status check 수신
                    logPrintInTextBox("TOPIC = " + str2 + "에 대해 상태 요청을 수신하였습니다.", "");

                    string txData2 = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " response";
                    this.sendDataOut(commands["responemsgsvr"] + str2 + "," + txData2.Length + "," + txData2);
                    if (lbActionState.Text == states.onem2mtc0213031.ToString())
                        lbActionState.Text = states.onem2mtc0213032.ToString();
                    else
                    lbActionState.Text = states.responemsgsvr.ToString();
                }
                else if (nextresponse != string.Empty)
                {
                    if (rxMsg.StartsWith(nextresponse, System.StringComparison.CurrentCultureIgnoreCase))
                    {
                        // 타겟으로 하는 문자열(s, 고정 값)과 이후 문자열(str2, 변하는 값)을 구분함.
                        int first = rxMsg.IndexOf(nextresponse) + nextresponse.Length;
                        string str2 = "";
                        str2 = rxMsg.Substring(first, rxMsg.Length - first);

                        this.parseNextReceiveData(str2);
                    }
                }
                else if (rxMsg.StartsWith("AT") == false)
                    this.parseNoPrefixData(rxMsg);
            }
        }

        private void parseNextReceiveData(string str2)
        {
            string[] rcvdatas = {string.Empty, string.Empty};    // 수신한 데이터를 한 문장씩 나누어 array에 저장
            string txData = string.Empty;

            states state = (states)Enum.Parse(typeof(states), lbActionState.Text);
            switch (state)
            {
                case states.getmodemver:
                    lbModemVer.Text = dev.version = str2;
                    lbActionState.Text = states.idle.ToString();
                    this.logPrintInTextBox("모뎀버전이 " + dev.version + "로 저장되었습니다.", "");

                    break;
                case states.autogetmodemver:
                    lbModemVer.Text = dev.version = str2;
                    progressBar1.Value = 100;
                    this.logPrintInTextBox("모뎀버전이 " + dev.version + "로 저장되었습니다.", "");

                    nextcommand = states.autogetimsi.ToString();
                    break;
                case states.geticcid:
                case states.autogeticcid:
                    string[] strchs = str2.Split(' ');        // Remove first char ' '
                    if (strchs.Length > 1)
                        str2 = strchs[strchs.Length - 1];

                    if (str2.Length > 19)
                        lbIccid.Text = dev.iccid = str2.Substring(str2.Length - 20, 19);
                    else
                        lbIccid.Text = dev.iccid = str2;

                    logPrintInTextBox("ICCID가 " + dev.iccid + "로 저장되었습니다.", "");

                    setDeviceEntityID();

                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.getdevip:
                    textBox3.Text = str2.Replace("\"", "");
                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.modemFWUPmodechk:
                    if (str2 == "5")
                        nextcommand = states.modemFWUPmodeset.ToString();
                    else
                        nextcommand = states.modemFWUPboot.ToString();
                    lbActionState.Text = states.modemFWUPmodechked.ToString();
                    break;
                case states.onem2mtc0211034:
                    if (str2 == "5")
                        nextcommand = states.onem2mtc0211041.ToString();
                    else
                        nextcommand = states.onem2mtc0211040.ToString();
                    lbActionState.Text = states.onem2mtc0211035.ToString();
                    break;
                case states.resetmodechk:
                    if (str2 == "5")
                        nextcommand = states.resetmefauth.ToString();
                    else
                        nextcommand = states.resetmodeset.ToString();
                    lbActionState.Text = states.resetmodechked.ToString();
                    break;
                case states.getserverinfo:
                    // oneM2M server 정보 확인
                    string[] servers = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장

                    if (lbActionState.Text == states.getserverinfo.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["getonem2mmode"]);
                        lbActionState.Text = states.onem2mtc0201021.ToString();
                        nextresponse = "$LGTMPF=";
                    }
                    break;
                case states.setmefauth:
                    // oneM2M 인증 결과
                    if (str2 == "2000")
                    {
                            lbActionState.Text = states.idle.ToString();
                    }
                    else
                    {
                        lbActionState.Text = states.idle.ToString();
                    }
                    break;
                case states.fotamefauthnt:
                    // oneM2M 인증 결과
                    if (str2 == "2000")
                    {
                        this.sendDataOut(commands["deviceFWUPfinish"]);
                        lbActionState.Text = states.deviceFWUPfinish.ToString();
                        nextresponse = "$OM_DEV_FWUP_FINISH=";
                    }
                    else
                        lbActionState.Text = states.idle.ToString();
                    break;
                case states.onem2mtc0210041:
                    // oneM2M 인증 결과
                    if (str2 == "2000")
                    {
                        this.sendDataOut(commands["deviceFWUPfinish"]);
                        nextresponse = "$OM_DEV_FWUP_FINISH=";
                        lbActionState.Text = states.onem2mtc0210042.ToString();
                    }
                    else
                    {
                        lbActionState.Text = states.idle.ToString();
                    }
                    break;
                case states.onem2mtc0210042:
                    if (str2 == "2004" || str2 == "2000")
                    {
                        if (svr.enrmtKeyId != string.Empty)
                        {
                            RetriveDverToPlatform();
                        }
                    }

                    this.sendDataOut(commands["getmodemSvrVer"]);
                    lbActionState.Text = states.onem2mtc021101.ToString();
                    nextresponse = "$OM_MODEM_FWUP_RSP=";
                    break;
                case states.deviceFWUPfinish:
                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.mfotamefauth:
                    // oneM2M 인증 결과
                        lbActionState.Text = states.idle.ToString();
                    break;
                case states.modemFWUPreport:
                    if (lbActionState.Text == states.modemFWUPreport.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["delsubscript"] + "StoD");
                        lbActionState.Text = states.onem2mtc0212022.ToString();
                        nextresponse = "$OM_D_SUB_RSP=";
                    }
                    break;
                case states.getCSEbase:
                    // oneM2M CSEBase 조회 결과
                    if (lbActionState.Text == states.getCSEbase.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["getremoteCSE"]);
                        lbActionState.Text = states.onem2mtc020301.ToString();
                        nextresponse = "$OM_R_CSE_RSP=";
                    }
                    break;
                case states.getremoteCSE:
                    // oneM2M remoteCSE 조회 결과, 4004이면 생성/2000 또는 2004이면 container 확인
                    if (str2 == "4004")
                    {
                        if (lbActionState.Text == states.getremoteCSE.ToString())
                        {
                            MessageBox.Show("remote CSE가 존재하지 않습니다.");
                            lbActionState.Text = states.idle.ToString();
                        }
                        else
                        {
                            this.sendDataOut(commands["setremoteCSE"]);
                            lbActionState.Text = states.onem2mtc0205012.ToString();     // 신규 삭제
                            nextresponse = "$OM_C_CSE_RSP=";
                        }
                    }
                    else if (str2 == "2000")
                    {

                        if (lbActionState.Text == states.getremoteCSE.ToString())
                            lbActionState.Text = states.idle.ToString();
                        else
                        {
                            this.sendDataOut(commands["updateremoteCSE"]);
                            lbActionState.Text = states.onem2mtc020505.ToString();
                            nextresponse = "$OM_U_CSE_RSP=";
                        }
                    }
                    break;
                case states.setremoteCSE:
                    // oneM2M remoteCSE 생성 결과, 2001이면 container 생성 요청
                    if (lbActionState.Text == states.setremoteCSE.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["setcontainer"]);
                        lbActionState.Text = states.onem2mtc0205021.ToString();
                        nextresponse = "$OM_C_CON_RSP=";
                    }
                    break;
                case states.onem2mset01:
                    // oneM2M remoteCSE 생성 결과, 2001이면 container 생성 요청
                    this.sendDataOut(commands["setcontainer"]);
                    lbActionState.Text = states.onem2mset02.ToString();
                    nextresponse = "$OM_C_CON_RSP=";
                    break;
                case states.updateremoteCSE:
                case states.onem2mtc020505:
                    // oneM2M remoteCSE 업데이트 결과, 2004이면 remoteCSE (poa) 업데이트 성공
                    if (lbActionState.Text == states.updateremoteCSE.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        // Data send to SERVER (string original)
                        //AT$OM_C_INS_REQ=<object>,<length>,<data>
                        txData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " oneM2M device";
                        if (svr.enrmtKeyId != string.Empty)
                        {
                            lbActionState.Text = states.onem2mtc0205041.ToString();
                            this.sendDataOut(commands["sendonemsgstr"] + "DtoS" + "," + txData.Length + "," + txData);
                        }
                        else
                        {
                            lbActionState.Text = states.onem2mtc0205043.ToString();
                            this.sendDataOut(commands["sendonemsgstr"] + "StoD" + "," + txData.Length + "," + txData);
                        }
                        nextresponse = "$OM_C_INS_RSP=";
                    }
                    break;
                case states.delremoteCSE:
                case states.onem2mtc0212042:
                    // oneM2M remoteCSE 삭제 결과, 2002이면 성공
                    if (lbActionState.Text == states.delremoteCSE.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["setremoteCSE"]);
                        lbActionState.Text = states.onem2mset01.ToString();
                        nextresponse = "$OM_C_CSE_RSP=";
                    }
                    break;
                case states.setcontainer:
                case states.onem2mtc0205021:
                    // oneM2M container 생성 결과, 2001이면 성공
                    if (str2 == "2001")
                    {
                        this.sendDataOut(commands["settxcontainer"]);
                        nextresponse = "$OM_C_CON_RSP=";
                        if (lbActionState.Text == states.setcontainer.ToString())
                            lbActionState.Text = states.settxcontainer.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0205022.ToString();
                    }
                    else if (str2 == "4105")
                    {
                        if (lbActionState.Text == states.setcontainer.ToString())
                        {
                            MessageBox.Show("동일한 폴더 이름이 있습니다.");
                            this.sendDataOut(commands["settxcontainer"]);
                            lbActionState.Text = states.settxcontainer.ToString();
                            nextresponse = "$OM_C_CON_RSP=";
                        }
                        else
                        {
                            this.sendDataOut(commands["settxcontainer"]);
                            nextresponse = "$OM_C_CON_RSP=";
                            lbActionState.Text = states.onem2mtc0205022.ToString();
                        }
                    }
                    else
                    {

                        this.sendDataOut(commands["settxcontainer"]);
                        nextresponse = "$OM_C_CON_RSP=";
                        if (lbActionState.Text == states.onem2mtc0205021.ToString())
                            lbActionState.Text = states.onem2mtc0205022.ToString();
                    }
                    break;
                case states.onem2mset02:
                    // oneM2M container 생성 결과, 2001이면 성공

                    this.sendDataOut(commands["settxcontainer"]);
                    nextresponse = "$OM_C_CON_RSP=";
                    lbActionState.Text = states.onem2mset03.ToString();
                    break;
                case states.settxcontainer:
                    if (lbActionState.Text == states.settxcontainer.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["setsubscript"] + "StoD");
                        lbActionState.Text = states.onem2mtc0205031.ToString();
                        nextresponse = "$OM_C_SUB_RSP=";
                    }
                    break;
                case states.onem2mset03:
                    this.sendDataOut(commands["setsubscript"] + "StoD");
                    lbActionState.Text = states.onem2mset04.ToString();
                    nextresponse = "$OM_C_SUB_RSP=";
                    break;
                case states.delcontainer:
                    // oneM2M container 삭제 결과, 2002이면 성공
                    this.sendDataOut(commands["delcontainer"] + "DtoS");
                    if (lbActionState.Text == states.delcontainer.ToString())
                        lbActionState.Text = states.delcontainer2.ToString();
                    else
                        lbActionState.Text = states.onem2mtc0212032.ToString();
                    nextresponse = "$OM_D_CON_RSP=";
                    break;
                case states.delcontainer2:
                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.onem2mtc0212032:
                    this.sendDataOut(commands["delremoteCSE"]);
                    lbActionState.Text = states.onem2mtc0212042.ToString();
                    nextresponse = "$OM_D_CSE_RSP=";
                    break;
                case states.onem2mset04:
                    // oneM2M subscription 신청 결과

                    string kind = "type=onem2m";
                    kind += "&ctn=" + tbDeviceCTN.Text;
                    kind += "&from=" + tcStartTime.ToString("yyyyMMddHHmmss");
                    getSvrLoglists(kind, "auto");

                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.delsubscript:
                    if (lbActionState.Text == states.delsubscript.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        this.sendDataOut(commands["setsubscript"] + "StoD");
                        lbActionState.Text = states.onem2mtc0205032.ToString();
                        nextresponse = "$OM_C_SUB_RSP=";
                    }
                    break;
                case states.sendonemsgstr:
                    // 플랫폼 서버에 data 송신
                    rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                    if (rcvdatas[0] == "2001")
                    {
                        if (svr.enrmtKeyId != string.Empty)
                        {
                            lbActionState.Text = states.sendonemsgstrchk.ToString();
                            RetriveDataToPlatform();
                        }
                        else
                        {
                            lbActionState.Text = states.idle.ToString();
                        }
                    }
                    else
                        lbActionState.Text = states.idle.ToString();
                    break;
                case states.onem2mtc0205041:
                    // 플랫폼 서버에 data 송신
                    rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                    if (rcvdatas[0] == "2001")
                    {
                        lbActionState.Text = states.onem2mtc0205042.ToString();
                        RetriveDataToPlatform();
                    }
                    else if (rcvdatas[0] == "4004")
                    {
                        this.sendDataOut(commands["setcontainer"]);
                        lbActionState.Text = states.onem2mtc0205021.ToString();
                        nextresponse = "$OM_C_CON_RSP=";
                    }
                    else
                    {
                        lbActionState.Text = states.onem2mtc0206011.ToString();
                        SendDataToPlatform();
                    }
                    break;
                case states.getonem2mdata:
                case states.sendonedevdb2:
                    // 플랫폼 서버에 data 수신
                    rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                    if (rcvdatas[0] == "2000")
                    {
                        // 수신한 데이터 사이즈 확이
                        int rxdatasize = Convert.ToInt32(rcvdatas[1]);
                        if (rxdatasize == rcvdatas[2].Length)
                        {
                            logPrintInTextBox(rcvdatas[2] + "를 수신하였습니다.", "");
                        }
                        else
                            MessageBox.Show("수신한 데이터 사이즈를 확인하세요", "");
                    }
                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.onem2mtc0207011:
                case states.onem2mtc0207012:
                    rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                    if (rcvdatas[0] == "2000")
                    {
                        // 수신한 데이터 사이즈 확이
                        int rxdatasize = Convert.ToInt32(rcvdatas[1]);
                        if (rxdatasize == rcvdatas[2].Length)
                        {
                            logPrintInTextBox(rcvdatas[2] + "를 수신하였습니다.", "");
                        }
                    }
                    break;
                case states.sendonemsgsvr:
                    // oneM2M data forwarding 요청 결과, 2001이면 
                    if (lbActionState.Text == states.sendonemsgsvr.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        lbActionState.Text = states.onem2mtc021302.ToString();

                        rTh = new Thread(new ThreadStart(SendDataToOneM2M));
                        rTh.Start();
                    }
                    break;
                case states.deviceFWUPstart:
                case states.onem2mtc0210034:
                    oneM2Mtotalsize = Convert.ToUInt32(str2);
                    logPrintInTextBox("FOTA 이미지 크기는 " + str2 + "입니다.", "");
                    if (lbActionState.Text == states.deviceFWUPstart.ToString())
                        lbActionState.Text = states.deviceFWDLfinsh.ToString();
                    else
                        lbActionState.Text = states.onem2mtc0210035.ToString();
                    nextresponse = "$OM_DEV_FWDL_FINISH";
                    break;
                case states.deviceFWDownload:
                case states.onem2mtc0210031:
                    oneM2Mtotalsize = Convert.ToUInt32(str2);
                    logPrintInTextBox("FOTA 이미지 크기는 " + str2 + "입니다.", "");
                    if (lbActionState.Text == states.deviceFWDownload.ToString())
                        lbActionState.Text = states.deviceFWDownloading.ToString();
                    break;
                case states.deviceFWDownloading:
                    rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장
                        oneM2Mrcvsize += Convert.ToUInt32(rcvdatas[0]);
                        logPrintInTextBox("index= " + oneM2Mrcvsize + "/" + oneM2Mtotalsize + "를 수신하였습니다.", "");
                        if (rcvdatas[0] != "512" || oneM2Mrcvsize >= oneM2Mtotalsize)
                        {
                            if (lbActionState.Text == states.deviceFWDownloading.ToString())
                                lbActionState.Text = states.deviceFWDLfinsh.ToString();
                            else
                                lbActionState.Text = states.onem2mtc0210033.ToString();
                            nextresponse = "$OM_DEV_FWDL_FINISH";
                        }
                    break;
                case states.deviceFWList:
                    rcvdatas = str2.Split(',');    // 수신한 데이터를 한 문장씩 나누어 array에 저장

                    if (oneM2Mtotalsize == Convert.ToUInt32(rcvdatas[1]))
                    {
                        nextcmdexts = rcvdatas[0];
                        if (lbActionState.Text == states.deviceFWList.ToString())
                            nextcommand = states.deviceFWOpen.ToString();
                        else
                            nextcommand = states.onem2mtc0210037.ToString();
                    }
                    break;
                case states.deviceFWOpen:
                    filecode = str2;
                    nextcmdexts = filecode + ",512";
                    if (lbActionState.Text == states.deviceFWOpen.ToString())
                        nextcommand = states.deviceFWRead.ToString();
                    else
                        nextcommand = states.onem2mtc0210038.ToString();
                    break;
                case states.deviceFWRead:
                    oneM2Mrcvsize += Convert.ToUInt32(str2);
                    logPrintInTextBox("index= " + oneM2Mrcvsize + "/" + oneM2Mtotalsize + "를 수신하였습니다.", "");
                    if (str2 != "512" || oneM2Mrcvsize >= oneM2Mtotalsize)
                    {
                        nextcmdexts = filecode;
                        if (lbActionState.Text == states.deviceFWRead.ToString())
                            nextcommand = states.deviceFWClose.ToString();
                        else
                            nextcommand = states.onem2mtc0210039.ToString();
                    }
                    else
                    {
                        nextcmdexts = filecode + ",512";
                        if (lbActionState.Text == states.deviceFWRead.ToString())
                            nextcommand = states.deviceFWRead.ToString();
                        else
                            nextcommand = states.onem2mtc0210038.ToString();
                    }
                    break;
                case states.modemFWUPstart:
                    logPrintInTextBox("MODEM FOTA 다운로드 완료되었습니다.", "");
                        lbActionState.Text = states.modemFWUPfinish.ToString();
                    break;
                case states.modemFWUPfinish:

                    //doCloseComPort();
                    //doOpenComPort();

                    logPrintInTextBox("MODEM 업데이트가 완료되었습니다.", "");
                    if (lbActionState.Text == states.modemFWUPfinish.ToString())
                    {
                        this.sendDataOut(commands["getonem2mmode"]);
                        lbActionState.Text = states.modemFWUPmodechk.ToString();
                        nextresponse = "$LGTMPF=";
                    }
                    else
                    {
                        lbActionState.Text = states.onem2mtc0211033.ToString();
                        timer2.Interval = 10000;
                        timer2.Start();
                    }
                    break;
                default:
                    lbActionState.Text = states.idle.ToString();
                    break;
            }

            if (lbActionState.Text == states.idle.ToString())
                nextresponse = string.Empty;
        }

        private void OKReceived()
        {
            string txData = string.Empty;

            states state = (states)Enum.Parse(typeof(states), lbActionState.Text);
            switch (state)
            {
                case states.deviceFWClose:
                    // 디바이스 펌웨어 버전 등록을 위해 플랫폼 서버 MEF AUTH 요청
                    this.sendDataOut(commands["setmefauth"] + tbSvcCd.Text + "," + tBoxDeviceModel.Text + "," + tBoxDeviceVer.Text + "," + tBoxDeviceSN.Text);
                    if (lbActionState.Text == states.deviceFWClose.ToString())
                        lbActionState.Text = states.fotamefauthnt.ToString();
                    else
                        lbActionState.Text = states.onem2mtc0210041.ToString();
                    nextresponse = "$OM_AUTH_RSP=";
                    break;
                case states.modemFWUPboot:
                case states.modemFWUPmodechk:               // $$LGTMPF= 응답 없이 OK가 응답된 경우 예외처리를 위해
                    this.sendDataOut(commands["getonem2mmode"]);
                    lbActionState.Text = states.modemFWUPmodechk.ToString();
                    nextresponse = "$LGTMPF=";
                    break;
                case states.onem2mtc0211040:
                    this.sendDataOut(commands["getonem2mmode"]);
                    lbActionState.Text = states.onem2mtc0211034.ToString();
                    nextresponse = "$LGTMPF=";
                    break;
                case states.resetmodechk:
                    this.sendDataOut(commands["getonem2mmode"]);
                    lbActionState.Text = states.resetmodechk.ToString();
                    nextresponse = "$LGTMPF=";
                    break;
                case states.onem2mtc0211034:
                    this.sendDataOut(commands["getonem2mmode"]);
                    lbActionState.Text = states.onem2mtc0211034.ToString();
                    nextresponse = "$LGTMPF=";
                    break;
                case states.setrcvauto:

                    if (lbActionState.Text == states.setrcvauto.ToString())
                        lbActionState.Text = states.idle.ToString();
                    else
                    {
                        if (svr.enrmtKeyId != string.Empty)
                        {
                            lbActionState.Text = states.onem2mtc0206031.ToString();
                            SendDataToPlatform();
                            nextcommand = string.Empty;
                        }
                        else
                        {
                            lbActionState.Text = states.onem2mtc0206032.ToString();
                            txData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " oneM2M device";
                            this.sendDataOut(commands["sendonemsgstr"] + "StoD" + "," + txData.Length + "," + txData);
                        }
                    }
                    break;
                case states.setrcvmanu:
                    nextcommand = string.Empty;
                    break;
                case states.onem2mtc0206041:        // 자동수신 이벤트를 먼저받아서 수동 수신 모드로 설정 송신 시험부터 재시험
                    // Data send to SERVER (string original)
                    //AT$OM_C_INS_REQ=<object>,<length>,<data>
                    txData = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff") + " oneM2M device";
                    if (svr.enrmtKeyId != string.Empty)
                    {
                        lbActionState.Text = states.onem2mtc0205041.ToString();
                        this.sendDataOut(commands["sendonemsgstr"] + "DtoS" + "," + txData.Length + "," + txData);
                    }
                    else
                    {
                        lbActionState.Text = states.onem2mtc0205043.ToString();
                        this.sendDataOut(commands["sendonemsgstr"] + "StoD" + "," + txData.Length + "," + txData);
                    }
                    nextresponse = "$OM_C_INS_RSP=";
                    break;
                default:
                    break;
            }

            // 마지막 응답(OK)을 받은 후에 후속 작업이 필요한지 확인한다.
            if (nextcommand != string.Empty)
            {
                states nextstate = (states)Enum.Parse(typeof(states), nextcommand);
                switch (nextstate)
                {
                    // 단말 정보 자동 갱신 순서
                    // autogetmanufac - (autogetmodel) - autogetimei - autogetmodemver
                    case states.autogetmodel:
                        this.sendDataOut(textBox47.Text);
                        lbActionState.Text = states.autogetmodel.ToString();
                        break;
                    // 단말 정보 자동 갱신 순서
                    // autogetmanufac - autogetmodel - (autogetimei) - autogetmodemver
                    case states.autogetimei:
                        this.sendDataOut(textBox49.Text);
                        nextresponse = textBox40.Text;
                        lbActionState.Text = states.autogetimei.ToString();
                        break;
                    // 단말 정보 자동 갱신 순서
                    // autogetmanufac - autogetmodel - autogetimei - (autogetmodemver)
                    case states.autogetmodemver:
                        this.sendDataOut(textBox44.Text);
                        nextresponse = textBox57.Text;
                        lbActionState.Text = states.autogetmodemver.ToString();
                        break;
                    // 단말 정보 자동 갱신 순서
                    // autogetmanufac - autogetmodel - autogetimei - autogetmodemver - (autoimsi)
                    case states.autogetimsi:

                        this.sendDataOut(textBox46.Text);
                        nextresponse = textBox33.Text;
                        lbActionState.Text = states.autogetimsi.ToString();
                        break;
                    case states.autogeticcid:
                        this.sendDataOut(textBox45.Text);
                        nextresponse = textBox38.Text;
                        lbActionState.Text = states.autogeticcid.ToString();
                        break;
                    case states.seteventalt1:
                        this.sendDataOut("AT%LWM2MOPEV=1,20");
                        lbActionState.Text = states.seteventalt2.ToString();
                        break;
                    case states.deviceFWOpen:
                        this.sendDataOut(commands["deviceFWOpen"] + nextcmdexts);
                        if (nextstate == states.deviceFWOpen)
                            lbActionState.Text = states.deviceFWOpen.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0210037.ToString();
                        nextresponse = "+QFOPEN: ";
                        nextcmdexts = string.Empty;
                        break;
                    case states.deviceFWRead:
                    case states.onem2mtc0210038:
                        this.sendDataOut(commands["deviceFWRead"] + nextcmdexts);
                        if (nextstate == states.deviceFWRead)
                            lbActionState.Text = states.deviceFWRead.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0210038.ToString();
                        nextresponse = "CONNECT ";
                        nextcmdexts = string.Empty;
                        break;
                    case states.deviceFWClose:
                    case states.onem2mtc0210039:
                        this.sendDataOut(commands["deviceFWClose"] + nextcmdexts);
                        if (nextstate == states.deviceFWClose)
                            lbActionState.Text = states.deviceFWClose.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0210039.ToString();
                        nextcmdexts = string.Empty;
                        break;
                    case states.modemFWUPboot:
                        this.sendDataOut(commands["setonem2mmode"]);
                        if (nextstate == states.modemFWUPboot)
                            lbActionState.Text = states.modemFWUPboot.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0211040.ToString();
                        nextresponse = "$LGTMPF=";
                        break;
                    case states.modemFWUPmodeset:
                        // 디바이스 펌웨어 버전 등록을 위해 플랫폼 서버 MEF AUTH 요청
                        this.sendDataOut(commands["setmefauth"] + tbSvcCd.Text + "," + tBoxDeviceModel.Text + "," + tBoxDeviceVer.Text + "," + tBoxDeviceSN.Text);
                        if (nextstate == states.modemFWUPmodeset)
                            lbActionState.Text = states.mfotamefauth.ToString();
                        else
                            lbActionState.Text = states.onem2mtc0211042.ToString();
                        nextresponse = "$OM_AUTH_RSP=";
                        break;
                    case states.resetmodeset:
                        this.sendDataOut(commands["setonem2mmode"]);
                        lbActionState.Text = states.resetmodeseted.ToString();
                        break;
                    case states.resetmefauth:

                        // RESET 상태 등록을 위해 플랫폼 서버 MEF AUTH 요청
                        this.sendDataOut(commands["setmefauth"] + tbSvcCd.Text + "," + tBoxDeviceModel.Text + "," + tBoxDeviceVer.Text + "," + tBoxDeviceSN.Text);
                        lbActionState.Text = states.resetmefauth.ToString();
                        nextresponse = "$OM_AUTH_RSP=";
                        break;
                    default:
                        break;
                }
                nextcommand = string.Empty;
            }
        }

        private void parseNoPrefixData(string str1)
        {
            states state = (states)Enum.Parse(typeof(states), lbActionState.Text);
            switch (state)
            {
                case states.getmanufac:
                    textBox85.Text = dev.maker = str1;
                    lbActionState.Text = states.idle.ToString();
                    this.logPrintInTextBox("제조사값이 " + dev.maker + "로 저장되었습니다.", "");
                    break;
                // 단말 정보 자동 갱신 순서
                // (autogetmanufac) - (autogetmodel) - autogetimei - autogetmodemver
                case states.autogetmanufac:
                    textBox85.Text = dev.maker = str1;
                    progressBar1.Value = 60;
                    this.logPrintInTextBox("제조사값이 " + dev.maker + "로 저장되었습니다.", "");
                    nextcommand = states.autogetmodel.ToString();
                    break;
                case states.getmodel:
                    tBoxDeviceModel.Text = textBox86.Text = dev.model = str1;
                    this.logPrintInTextBox("모델값이 " + dev.model + "로 저장되었습니다.", "");
                    break;
                // 단말 정보 자동 갱신 순서
                // autogetmanufac - (autogetmodel) - (autogetimei) - autogetmodemver
                case states.autogetmodel:
                    tBoxDeviceModel.Text = textBox86.Text = dev.model = str1;
                    progressBar1.Value = 70;
                    this.logPrintInTextBox("모델값이 " + dev.model + "로 저장되었습니다.", "");

                    setModelConfig(str1);
                    nextcommand = states.autogetimei.ToString();
                    break;
                case states.getimei:
                    textBox89.Text = dev.imei = str1.Replace("\"", "");
                    logPrintInTextBox("IMEI를 " + dev.imei + "로 저장하였습니다.", "");

                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.autogetimei:
                    // 단말 정보 자동 갱신 순서
                    // autogetmanufac - autogetmodel - (autogetimei) - (autogetmodemver)
                    textBox89.Text = dev.imei = str1.Replace("\"", "");
                    logPrintInTextBox("IMEI를 " + dev.imei + "로 저장하였습니다.", "");
                    progressBar1.Value = 90;

                    nextcommand = states.autogetmodemver.ToString();       // 모듈 정보를 모두 읽고 모뎀 버전 정보 조회
                    break;
                case states.getimsi:
                    textBox87.Text = str1;
                    if (str1.StartsWith("45006"))
                    {
                        string ctn = "0" + str1.Substring(5, str1.Length - 5);

                        tbDeviceCTN.Text = dev.imsi = ctn;
                        this.logPrintInTextBox("IMSI값이 " + dev.imsi + "로 저장되었습니다.", "");
                    }
                    else
                        this.logPrintInTextBox("USIM 상태 확인이 필요합니다.", "");

                    lbActionState.Text = states.idle.ToString();
                    break;
                case states.autogetimsi:
                    textBox87.Text = str1;
                    if (str1.StartsWith("45006"))
                    {
                        string ctn = "0" + str1.Substring(5, str1.Length - 5);

                        tbDeviceCTN.Text = dev.imsi = ctn;
                        this.logPrintInTextBox("IMSI값이 " + dev.imsi + "로 저장되었습니다.", "");
                    }
                    else
                        this.logPrintInTextBox("USIM 상태 확인이 필요합니다.", "");

                    nextcommand = states.autogeticcid.ToString();
                    break;
                case states.getmodemver:
                    lbModemVer.Text = dev.version = str1;
                    lbActionState.Text = states.idle.ToString();
                    this.logPrintInTextBox("모뎀버전이 " + dev.version + "로 저장되었습니다.", "");

                    break;
                case states.autogetmodemver:
                    lbModemVer.Text = dev.version = str1;
                    progressBar1.Value = 100;
                    this.logPrintInTextBox("모뎀버전이 " + dev.version + "로 저장되었습니다.", "");

                    nextcommand = states.autogetimsi.ToString();
                    break;
                default:
                    break;
            }
        }

        private void setModelConfig(string model)
        {

        }

        private void getDeviveInfo()
        {
            this.logPrintInTextBox("DEVICE 정보 전체를 요청합니다.","");

            // 단말 정보 자동 갱신 순서
            // (autogetmanufac) - autogetmodel - autogetimei - autogetmodemver
            this.sendDataOut(textBox48.Text);
            lbActionState.Text = states.autogetmanufac.ToString();
        }

        private string BcdToString(char[] charValues)
        {
            string bcdstring = "";
            for (int i = 0; i < charValues.Length - 1;)
            {
                // Convert to the first character.
                int value = bcdvalues[charValues[i++]] * 0x10;

                // Convert to the second character.
                value += bcdvalues[charValues[i++]];

                // Convert the decimal value to a hexadecimal value in string form.
                bcdstring += Char.ConvertFromUtf32(value);
            }
            return bcdstring;
        }

       private void button63_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();

            saveFile.Title = "Excell 저장 파일";
            saveFile.InitialDirectory = Application.StartupPath; 
            saveFile.FileName = tBoxDeviceModel.Text + "_config.xls";
            saveFile.DefaultExt = "excell files(*.xls)|*.xls";
            saveFile.Filter = "excell files (*.xls)|*.xls";
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Workbook workbook = new Workbook();
                    Worksheet worksheet = new Worksheet("options");
                    int i = 0;
                    worksheet.Cells[i, 0] = new Cell("COM PORT");
                    worksheet.Cells[i, 1] = new Cell(cBoxCOMPORT.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell("BAUDRATE");
                    worksheet.Cells[i, 1] = new Cell(cBoxBaudRate.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell("DTR");
                    worksheet.Cells[i, 1] = new Cell(cbDTR.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell("RTS");
                    worksheet.Cells[i, 1] = new Cell(cbRTS.Text);

                    worksheet.Cells.ColumnWidth[0, 2] = 7000;
                    workbook.Worksheets.Add(worksheet);

                    i = 0;
                    worksheet = new Worksheet("atcommand3");
                    worksheet.Cells[i, 0] = new Cell(label18.Text);
                    worksheet.Cells[i, 1] = new Cell(comboBox1.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button83.Text);
                    worksheet.Cells[i, 1] = new Cell("getmanufac");
                    worksheet.Cells[i, 2] = new Cell(textBox48.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button91.Text);
                    worksheet.Cells[i, 1] = new Cell("getmodel");
                    worksheet.Cells[i, 2] = new Cell(textBox47.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button90.Text);
                    worksheet.Cells[i, 1] = new Cell("getimsi");
                    worksheet.Cells[i, 2] = new Cell(textBox46.Text);
                    worksheet.Cells[i, 3] = new Cell(textBox33.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button71.Text);
                    worksheet.Cells[i, 1] = new Cell("geticcid");
                    worksheet.Cells[i, 2] = new Cell(textBox45.Text);
                    worksheet.Cells[i, 3] = new Cell(textBox38.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button89.Text);
                    worksheet.Cells[i, 1] = new Cell("getimei");
                    worksheet.Cells[i, 2] = new Cell(textBox49.Text);
                    worksheet.Cells[i, 3] = new Cell(textBox40.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button88.Text);
                    worksheet.Cells[i, 1] = new Cell("getmodemver");
                    worksheet.Cells[i, 2] = new Cell(textBox44.Text);
                    worksheet.Cells[i, 3] = new Cell(textBox57.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button86.Text);
                    worksheet.Cells[i, 1] = new Cell("rfreset");
                    worksheet.Cells[i, 2] = new Cell(textBox24.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button99.Text);
                    worksheet.Cells[i, 1] = new Cell("setcereg");
                    worksheet.Cells[i, 2] = new Cell(textBox58.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button100.Text);
                    worksheet.Cells[i, 1] = new Cell("rfon");
                    worksheet.Cells[i, 2] = new Cell(textBox59.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button101.Text);
                    worksheet.Cells[i, 1] = new Cell("rfoff");
                    worksheet.Cells[i, 2] = new Cell(textBox60.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button62.Text);
                    worksheet.Cells[i, 1] = new Cell("getcereg");
                    worksheet.Cells[i, 2] = new Cell(textBox61.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button119.Text);
                    worksheet.Cells[i, 1] = new Cell("");
                    worksheet.Cells[i, 2] = new Cell(textBox78.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button63.Text);
                    worksheet.Cells[i, 1] = new Cell("");
                    worksheet.Cells[i, 2] = new Cell(textBox64.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button120.Text);
                    worksheet.Cells[i, 1] = new Cell("");
                    worksheet.Cells[i, 2] = new Cell(textBox79.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button121.Text);
                    worksheet.Cells[i, 1] = new Cell("");
                    worksheet.Cells[i, 2] = new Cell(textBox80.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button128.Text);
                    worksheet.Cells[i, 1] = new Cell("");
                    worksheet.Cells[i, 2] = new Cell(textBox81.Text);
                    i++;
                    worksheet.Cells[i, 0] = new Cell(button37.Text);
                    worksheet.Cells[i, 1] = new Cell("");
                    worksheet.Cells[i, 2] = new Cell(textBox4.Text);
                    worksheet.Cells[i, 3] = new Cell(textBox5.Text);

                    worksheet.Cells.ColumnWidth[0, 3] = 5000;
                    workbook.Worksheets.Add(worksheet);

                    workbook.Save(saveFile.FileName.ToString());
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button62_Click(object sender, EventArgs e)
        {
            openExcelFile();
        }

        private void openExcelFile()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = "xls";
            ofd.InitialDirectory = Application.StartupPath;
            ofd.Filter = "excell files (*.xls)|*.xls";
            ofd.Title = "테스트 모델 정보 선택";
            ofd.ShowDialog();
            if (ofd.FileName.Length > 0)
            {
                try
                {
                    Workbook workbook = Workbook.Load(ofd.FileName);

                    Worksheet worksheet = workbook.Worksheets[0];
                        int i = 0;
                        string comport = worksheet.Cells[i, 1].ToString();
                        if (cBoxCOMPORT.Items.Contains(comport))
                            cBoxCOMPORT.SelectedItem = comport;
                        else
                            cBoxCOMPORT.SelectedIndex = 0;
                        i++;
                        cBoxBaudRate.Text = worksheet.Cells[i, 1].ToString();
                        i++;
                        if (worksheet.Cells[i, 1].ToString() == "false")
                            cbDTR.Checked = false;
                        else
                            cbDTR.Checked = true;
                        i++;
                        if (worksheet.Cells[i, 1].ToString() == "false")
                            cbRTS.Checked = false;
                        else
                            cbRTS.Checked = true;


                        /////////////////////////////////////////////// 플랫폼 검증 앱 공통 AT command
                        i = 0;
                        worksheet = workbook.Worksheets[1];
                        comboBox1.Text = worksheet.Cells[i, 1].ToString();
                        if (comboBox1.SelectedIndex == 1)
                            dev.type = "lwm2m";
                        else
                            dev.type = "onem2m";
                        i++;
                        textBox48.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox47.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox46.Text = worksheet.Cells[i, 2].ToString();
                        textBox33.Text = worksheet.Cells[i, 3].ToString();
                        i++;
                        textBox45.Text = worksheet.Cells[i, 2].ToString();
                        textBox38.Text = worksheet.Cells[i, 3].ToString();
                        i++;
                        textBox49.Text = worksheet.Cells[i, 2].ToString();
                        textBox40.Text = worksheet.Cells[i, 3].ToString();
                        i++;
                        textBox44.Text = worksheet.Cells[i, 2].ToString();
                        textBox57.Text = worksheet.Cells[i, 3].ToString();
                        i++;
                        textBox24.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox58.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox59.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox60.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox61.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox78.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox64.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox79.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox80.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox81.Text = worksheet.Cells[i, 2].ToString();
                        i++;
                        textBox4.Text = worksheet.Cells[i, 2].ToString();
                        textBox5.Text = worksheet.Cells[i, 3].ToString();
                }
                catch (Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void button83_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox48.Text);
            lbActionState.Text = states.getmanufac.ToString();
        }

        private void button91_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox47.Text);
            lbActionState.Text = states.getmodel.ToString();
        }

        private void button89_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox49.Text);
            nextresponse = textBox40.Text;
            lbActionState.Text = states.getimei.ToString();
        }

        private void button88_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox44.Text);
            nextresponse = textBox57.Text;
            lbActionState.Text = states.getmodemver.ToString();
        }

        private void button90_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox46.Text);
            nextresponse = textBox33.Text;
            lbActionState.Text = states.getimsi.ToString();
        }

        private void button71_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox45.Text);
            nextresponse = textBox38.Text;
            lbActionState.Text = states.geticcid.ToString();
        }

        private void button86_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox24.Text);
            lbActionState.Text = states.rfreset.ToString();
        }

        private void button87_Click(object sender, EventArgs e)
        {
            nextcommand = string.Empty;
            nextresponse = string.Empty;
            getDeviveInfo();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
                dev.type = "lwm2m";
            else
                dev.type = "onem2m";
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            openExcelFile();
        }

        private void button63_Click_1(object sender, EventArgs e)
        {
            this.sendDataOut(textBox64.Text);
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

                ListViewItem newitem = new ListViewItem(new string[] { time, lbActionState.Text, dir, data });
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

                        if (serialPort1.IsOpen == false)
                        {
                            if (iccId.ToString() != " ")
                            {
                                tBoxDeviceModel.Text = deviceModel.ToString();
                                textBox86.Text = dev.model = modemModel.ToString();
                                tbSvcCd.Text = serviceCode.ToString();
                                tBoxDeviceSN.Text = deviceSerialNo.ToString();

                                tbDeviceCTN.Text = dev.imsi = ctn.ToString();
                                lbIccid.Text = dev.iccid = iccId.ToString();
                                setDeviceEntityID();

                                MessageBox.Show("디바이스 모델명 : " + deviceModel.ToString() + "\n모듈 모델명 : " + modemModel.ToString() + "\n서비스코드 : "
                                    + serviceCode.ToString() + "\n디바이스 일련번호 : " + deviceSerialNo.ToString() + "\nICCID : " + iccId.ToString() + "\nTYPE : " + m2mmType.ToString(),
                                    ctn.ToString() + " DEVICE 상태 정보");
                            }
                            else
                                MessageBox.Show("디바이스 정보가 없습니다.\nhttps://testadm.onem2m.uplus.co.kr:8443 에서 확인바랍니다.", ctn.ToString() + " DEVICE 상태 정보");
                        }
                        else
                            MessageBox.Show("모듈이 연결된 상태에서는 동작하지 않습니다.");

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
                    logPrintInTextBox("Device EntityID가 " + dev.entityId + "수정되었습니다.", "");
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

                    lbActionState.Text = states.idle.ToString();
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

        private void button62_Click_1(object sender, EventArgs e)
        {
            this.sendDataOut(textBox61.Text);
            lbActionState.Text = states.getcereg.ToString();
        }

        private void button99_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox58.Text);
            lbActionState.Text = states.setcereg.ToString();
        }

        private void button100_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox59.Text);
        }

        private void button101_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox60.Text);
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!serialPort1.IsOpen)
            {
                progressBar1.Value = 0;

                if (lbActionState.Text != states.idle.ToString())
                    doOpenComPort();
                else if (progressBar1.Value != 0)
                {
                    logPrintInTextBox("잠시 후 COM 포트 재오픈이 필요합니다.", "");

                    lbActionState.Text = states.closed.ToString();
                    timer1.Stop();
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab.Name == "webpage")
            {
                if (webBrowser1.Url.ToString() == "about:blank")
                    webBrowser1.Navigate("https://testadm.onem2m.uplus.co.kr:8443");
            }
            else if (tabControl1.SelectedTab.Name == "tabCOM")
            {
                if (listView3.Items.Count > 35)
                    listView3.TopItem = listView3.Items[listView3.Items.Count - 1];
            }
            else if (tabControl1.SelectedTab.Name == "tabServer")
            {
                if (listView7.Items.Count > 35)
                    listView7.TopItem = listView7.Items[listView7.Items.Count - 1];
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lbActionState.Text == states.onem2mtc0211033.ToString())
            {
                this.sendDataOut(commands["getonem2mmode"]);
                lbActionState.Text = states.onem2mtc0211034.ToString();
                nextresponse = "$LGTMPF=";
            }
            else if (lbActionState.Text == states.resetboot.ToString())
            {
                this.sendDataOut(commands["getonem2mmode"]);
                lbActionState.Text = states.resetmodechk.ToString();
                nextresponse = "$LGTMPF=";
            }

            timer2.Stop();
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

                int i = 1;

                worksheet = new Worksheet("atcommand");
                worksheet.Cells[0, 0] = new Cell("시간");
                worksheet.Cells[0, 1] = new Cell("state");
                worksheet.Cells[0, 2] = new Cell(" ");
                worksheet.Cells[0, 3] = new Cell("내용");

                for (i=0;i<listView3.Items.Count;i++)
                {
                    worksheet.Cells[i+1, 0] = new Cell(listView3.Items[i].SubItems[0].Text);
                    worksheet.Cells[i+1, 1] = new Cell(listView3.Items[i].SubItems[1].Text);
                    worksheet.Cells[i+1, 2] = new Cell(listView3.Items[i].SubItems[2].Text);
                    worksheet.Cells[i+1, 3] = new Cell(listView3.Items[i].SubItems[3].Text);
                }

                worksheet.Cells.ColumnWidth[0] = 2500;
                worksheet.Cells.ColumnWidth[1] = 5000;
                worksheet.Cells.ColumnWidth[2] = 700;
                worksheet.Cells.ColumnWidth[3] = 30000;
                workbook.Worksheets.Add(worksheet);

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

        private void button119_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox78.Text);
        }

        private void button120_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox79.Text);
        }

        private void button121_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox80.Text);
        }

        private void button128_Click(object sender, EventArgs e)
        {
            this.sendDataOut(textBox81.Text);
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedListViewItemCollection itemColl = listView3.SelectedItems;
            foreach (ListViewItem item in itemColl)
            {
                textBox1.Text = item.SubItems[3].Text;
                textBox2.Text = item.SubItems[1].Text;
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
            string iccid = lbIccid.Text;
            if (iccid.Length > 6)
                bodymsg += iccid.Substring(iccid.Length - 6, 6);
            else
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

                ListViewItem newitem = new ListViewItem(new string[] { time, lbActionState.Text, dir, data });
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
            packetStr += "<vr>"+ lbModemVer.Text + "</vr>";
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
            packetStr += "<vr>" + lbModemVer.Text + "</vr>";
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
            header.Url = "http://" + oneM2MFOTAIP + ":" + oneM2MFOTAPort + "/ota/updateVersionCheck/moduleFirmware/" + tbSvcCd.Text + "/" + tBoxDeviceModel.Text + "/" + lbModemVer.Text;
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
            this.sendDataOut(textBox4.Text);
            nextresponse = textBox5.Text;
            lbActionState.Text = states.getdevip.ToString();
        }

        private void button56_Click_1(object sender, EventArgs e)
        {
            if (dev.remoteCSEName != string.Empty)
                RebootReport();
            else
                MessageBox.Show("단말인증파라미터 세팅하세요");
        }


        private void RebootReport()
        {
            ReqHeader header = new ReqHeader();
            header.Url = "http://" + oneM2MBRKIP + ":" + oneM2MBRKPort + "/IN_CSE-BASE-1/cb-1/" + dev.remoteCSEName + "/" + dev.nodeName + "/" + "rbo-m2m_" + tbDeviceCTN.Text;
            header.Method = "PUT";
            header.Accept = "application/vnd.onem2m-res+xml";
            header.ContentType = "application/vnd.onem2m-res+xml";
            header.X_M2M_RI = DateTime.Now.ToString("yyyyMMddHHmmss") + "Reboot_Report";
            header.X_M2M_Origin = dev.entityId;
            header.X_MEF_TK = dev.token;
            header.X_MEF_EKI = dev.enrmtKeyId;
            header.X_M2M_NM = string.Empty;
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

        private void cbDTR_CheckedChanged(object sender, EventArgs e)
        {
            if(cbDTR.Checked == true)
                serialPort1.DtrEnable = true;
            else
                serialPort1.DtrEnable = false;
        }

        private void cbRTS_CheckedChanged(object sender, EventArgs e)
        {
            if (cbRTS.Checked == true)
                serialPort1.RtsEnable = true;
            else
                serialPort1.RtsEnable = false;
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
