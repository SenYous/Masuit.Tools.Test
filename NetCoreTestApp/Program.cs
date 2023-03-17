using Masuit.Tools;
using Masuit.Tools.Database;
using Masuit.Tools.Html;
using System.Net;
using System.Numerics;
using System.Text.RegularExpressions;
using Masuit.Tools.Strings;
using Masuit.Tools.Systems;
using Masuit.Tools.DateTimeExt;
using Masuit.Tools.Linq;
using System.Data;
using Microsoft.AspNetCore.Builder;
using Masuit.Tools.Logging;
using Masuit.Tools.Core.Validator;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using Masuit.Tools.Reflection;
using Masuit.Tools.Media;
using Masuit.Tools.RandomSelector;
using System.Collections.Generic;
using Masuit.Tools.Models;
using System.Collections;
using Masuit.Tools.Maths;
using Masuit.Tools.Net;
using Masuit.Tools.Win32;
using System.Linq;
using Masuit.Tools.AspNetCore.Mime;
using SharpCompress.Compressors.Xz;
using Masuit.Tools.Files;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static System.Collections.Specialized.BitVector32;
using System.Web;
using System.Drawing;
using Masuit.Tools.Excel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SharpCompress.Common;
using Masuit.Tools.Files.FileDetector;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        Console.WriteLine("Hello, World!");

        #region 字符串扩展
        {
            DateTime toDatetime = "2023-03-13".ToDateTime();   // 字符串转时间
            Guid toGuid = "7dffbdd4-3881-45e1-bb4f-f2efeb8712fc".ToGuid();   // 字符串转GUID
            string replace = "abc123d456".Replace(new Regex("[0-9]"), "Z");  // 根据正则替换
            string createShortToken = "".CreateShortToken();  // 生成唯一短字符串
            long FromBase = "1001".FromBase(2); //任意进制转十进制
            BigInteger FromBaseBig = "1001".FromBaseBig(2); //任意进制转大数十进制

            // 检测字符串中是否包含列表中的关键词
            bool Contains = "ABCD".Contains(new string[] { "A" }, true);// 检测字符串中是否包含列表中的关键词(快速匹配)
            bool ContainsSafety = "ABCD".ContainsSafety(new string[] { "A" }, true);// 检测字符串中是否包含列表中的关键词(安全匹配)
            bool EndsWith = "ABCD".EndsWith(new string[] { "D" }, true);// 检测字符串中是否以列表中的关键词结尾
            bool StartsWith = "ABCD".StartsWith(new string[] { "A" }, true);// 检测字符串中是否以列表中的关键词开始
            bool RegexMatch = "ABCD".RegexMatch("D", true);// 检测字符串中是否包含列表中的关键词
            bool RegexMatch2 = "ABCD123".RegexMatch(new Regex("[0-9]"));// 检测字符串中是否包含列表中的关键词

            bool IsNullOrEmpty = "".IsNullOrEmpty();// 判断字符串是否为空或""
            string? nullStr = null;
            string AsNotNull = nullStr.AsNotNull();// 转成非null
            string IfNullOrEmpty = "".IfNullOrEmpty("123"); // 转成非null
            string IfNullOrEmpty2 = "".IfNullOrEmpty(GetTestString()); // 转成非null
            string Mask = "312000199502230660".Mask();   // 字符串掩码
            string Take = "ABCDEFG".Take(2);  // 取字符串前{length}个字
            int HammingDistance = "ABCDEFG".HammingDistance("ABCDEFG");   // 对比字符串的汉明距离

            // Email
            var (isMatch, match) = "337845818@qq.com".MatchEmail(); // 可在appsetting.json中添加EmailDomainWhiteList和EmailDomainBlockList配置邮箱域名黑白名单，逗号分隔，如"EmailDomainBlockList": "^\\w{1,5}@qq.com,^\\w{1,5}@163.com,^\\w{1,5}@gmail.com,^\\w{1,5}@outlook.com",
            var MatchEmailAsync = "337845818@qq.com".MatchEmailAsync(); // 匹配Email
            string MaskEmail = "337845818@qq.com".MaskEmail(); //邮箱掩码

            // 匹配URL
            Uri Matchurl = "http://ldqk.org/20/history".MatchUrl(out isMatch);  // 匹配完整格式的URL
            bool isUrl = "http://ldqk.org/20/history".MatchUrl();   // 匹配完整格式的URL
            bool IsExternalAddress = "http://ldqk.org/20/history".IsExternalAddress();    // 判断url是否是外部地址
            byte[] ToByteArray = "http://ldqk.org/20/history".ToByteArray(); // 转换成字节数组

            // 权威校验身份证号码
            bool isIdentifyCard = "362334199607150016".MatchIdentifyCard();// 校验中国大陆身份证号

            // IP 地址
            IPAddress MatchInetAddress = "114.114.114.114".MatchInetAddress(out isMatch);   // 校验IP地址的正确性
            bool isInetAddress = "114.114.114.114".MatchInetAddress(); // 校验IP
            uint IPToID = "114.114.114.114".IPToID();   // IP 转换成数字
            uint ToUInt32 = MatchInetAddress.ToUInt32(); // IP 转换成数字
            bool IsPrivateIP = "114.114.114.114".IsPrivateIP(); // 判断IP是否是私有地址
            bool IpAddressInRange = "114.114.114.114".IpAddressInRange("114", "114"); // 判断IP地址在不在某个IP地址段
            bool IpAddressInRange2 = MatchInetAddress.IpAddressInRange(MatchInetAddress, MatchInetAddress); // 判断IP地址在不在某个IP地址段

            // 校验手机号码的正确性
            bool isPhoneNumber = "15205201520".MatchPhoneNumber();  // 匹配手机号码

            // Crc32
            string Crc32 = "123456".Crc32();    // 获取字符串crc32签名
            string Crc64 = "123456".Crc64();    // 获取字符串Crc64签名

            // 中国专利申请号/专利号
            bool MatchCNPatentNumber = "200410018477.9".MatchCNPatentNumber();  // 校验中国专利申请号或专利号，是否带校验位，校验位前是否带“.”，都可以校验，待校验的号码前不要带CN、ZL字样的前缀
        }

        #endregion

        #region HTML操作
        {
            string html = @"<link href='/Content/font-awesome/css' rel='stylesheet'/>
        <!--[if IE 7]>
        <link href='/Content/font-awesome-ie7.min.css' rel='stylesheet'/>
        <![endif]-->
        <script src='/Scripts/modernizr'></script>
        <div id='searchBox' role='search'>
        <form action='/packages' method='get'>
        <span class='user-actions'><a href='/users/account/LogOff'>退出</a></span>
        <input name='q' id='searchBoxInput'/>
        <input id='searchBoxSubmit' type='submit' value='Submit' />
        </form>
        </div>";
            string html2 = "<div><img src=http://118.178.227.145:8022//Uploads/2023/202301/20230104/20230104024659148380.jpg\" class=\"el-image__inner el-image__preview\" style=\"object-fit: cover;\"><img src=\"http://118.178.227.145:8022//Uploads/2022/202212/20221206/20221206032619341941.jpg\" class=\"el-image__inner el-image__preview\" style=\"object-fit: cover;\"><img src=\"http://118.178.227.145:8022//Uploads/2023/202301/20230104/20230104024725051535.jpg\" class=\"el-image__inner el-image__preview\" style=\"object-fit: cover;\"></div>";
            //html的防XSS处理
            string s = html.HtmlSantinizerStandard(); // 清理后：<div><span><a href="/users/account/LogOff">退出</a></span></div>

            string RemoveHtmlTag = html.RemoveHtmlTag();  // 去除html标签后并截取字符串
            var MatchImgTags = html2.MatchImgTags(); // 匹配html的所有img标签集合
            var MatchImgSrcs = html2.MatchImgSrcs(); // 匹配html的所有img标签的src集合
            string MatchFirstImgSrc = html2.MatchFirstImgSrc(); // 获取html中第一个img标签的src
            string MatchRandomImgSrc = html2.MatchRandomImgSrc(); // 随机获取html代码中的img标签的src属性
            string MatchSeqRandomImgSrc = html2.MatchSeqRandomImgSrc(); // 按顺序优先获取html代码中的img标签的src属性
            string StrFormat = html.StrFormat();    // 替换回车换行符为html换行符
            string EncodeHtml = html.EncodeHtml();    // 替换html字符
        }

        #endregion

        #region 硬件监测
        {
            //float load = SystemInfo.CpuLoad;// 获取CPU占用率
            //long physicalMemory = SystemInfo.PhysicalMemory;// 获取物理内存总数
            //long memoryAvailable = SystemInfo.MemoryAvailable;// 获取物理内存可用率
            //double freePhysicalMemory = SystemInfo.GetFreePhysicalMemory();// 获取可用物理内存
            //double temperature = SystemInfo.GetCPUTemperature();// 获取CPU温度
            //int cpuCount = SystemInfo.GetCpuCount();// 获取CPU核心数
            //var ipAddress = SystemInfo.GetLocalIPs();// 获取本机所有IP地址
            //var localUsedIp = SystemInfo.GetLocalUsedIP();// 获取本机当前正在使用的IP地址
            //IList<string> macAddress = SystemInfo.GetMacAddress();// 获取本机所有网卡mac地址
            //string osVersion = Windows.GetOsVersion();// 获取操作系统版本
            //RamInfo ramInfo = SystemInfo.GetRamInfo();// 获取内存信息
            //var cpuSN = SystemInfo.GetCpuInfo()[0].SerialNumber; // CPU序列号
            //var driveSN = SystemInfo.GetDiskInfo()[0].SerialNumber; // 硬盘序列号

            //// 快速方法
            //var cpuInfos = CpuInfo.Locals; // 快速获取CPU的信息
            //var ramInfo2 = RamInfo.Local; // 快速获取内存的信息
            //var diskInfos = DiskInfo.Locals; // 快速获取硬盘的信息
            //var biosInfo = BiosInfo.Local; // 快速获取主板的信息
        }

        #endregion

        #region 整理Windows系统内存
        //Windows.ClearMemorySilent();
        #endregion

        #region 任意进制转换
        {
            NumberFormater nf = new NumberFormater(36);//内置2-62进制的转换
                                                       //NumberFormater nf = new NumberFormater("0123456789abcdefghijklmnopqrstuvwxyz");// 自定义进制字符，可用于生成验证码
            string s36 = nf.ToString(12345678);
            long num = nf.FromString("7clzi");
            Console.WriteLine("12345678的36进制是：" + s36); // 7clzi
            Console.WriteLine("36进制的7clzi是：" + num); // 12345678
            var sss = new NumberFormater(62).ToString(new Random().Next(100000, int.MaxValue)); //配合随机数生成随机字符串

            //扩展方法形式调用
            var bin = 12345678.ToBase(36);// 10进制转36进制：7clzi
            var FromBase2 = "7clzi".FromBase(36);// 36进制转10进制：12345678

            //超大数字的进制转换
            var num2 = "e6186159d38cd50e0463a55e596336bd".FromBaseBig(16); // 大数字16进制转10进制
            Console.WriteLine(num2); // 十进制：305849028665645097422198928560410015421
            Console.WriteLine(num2.ToBase(64)); // 64进制：3C665pQUPl3whzFlVpoPqZ，22位长度
            Console.WriteLine(num2.ToBase(36)); // 36进制：dmed4dkd5bhcg4qdktklun0zh，25位长度
            Console.WriteLine(num2.ToBase(7)); // 7进制：2600240311641665565300424545154525131265221035，46位长度
            Console.WriteLine(num2.ToBase(12)); // 12进制：5217744842749978a756b22135b16a5998a5，36位长度
            Console.WriteLine(num2.ToBase(41)); // 41进制：opzeBda2aytcEeudEquuesbk，24位长度
        }

        #endregion

        #region 纳秒级性能计时器
        {
            HiPerfTimer timer = HiPerfTimer.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                //todo
            }
            timer.Stop();
            Console.WriteLine("执行for循环100000次耗时" + timer.Duration + "s");

            double time = HiPerfTimer.Execute(() =>
            {
                for (int i = 0; i < 100000; i++)
                {
                    //todo
                }
            });
            Console.WriteLine("执行for循环100000次耗时" + time + "s");
        }
        #endregion

        #region 产生分布式唯一有序短id(雪花id)
        {
            //var sf = SnowFlake.GetInstance();
            //string token = sf.GetUniqueId();// rcofqodori0w
            //string shortId = sf.GetUniqueShortId(8);// qodw9728

            //var set = new HashSet<string>();
            //double time2 = HiPerfTimer.Execute(() =>
            //{
            //    for (int i = 0; i < 1000000; i++)
            //    {
            //        set.Add(SnowFlake.GetInstance().GetUniqueId());
            //    }
            //});
            //Console.WriteLine(set.Count == 1000000); //True
            //Console.WriteLine("产生100w个id耗时" + time + "s"); //2.6891495s
        }
        #endregion

        #region 农历转换
        {
            ChineseCalendar.CustomHolidays.Add(DateTime.Parse("2018-12-31"), "元旦节");//自定义节假日
            ChineseCalendar today = new ChineseCalendar(DateTime.Parse("1996-07-24"));
            Console.WriteLine(today.ChineseDateString);// 二零一八年十一月廿五
            Console.WriteLine(today.AnimalString);// 生肖：狗
            Console.WriteLine(today.GanZhiDateString);// 干支：戊戌年甲子月丁酉日
            Console.WriteLine(today.DateHoliday);// 获取按公历计算的节假日
            Console.WriteLine(today.Constellation);// 星座
            Console.WriteLine(ChineseCalendar.Today.NextDay.Date); // 明天
        }
        #endregion

        #region Linq表达式树扩展
        {
            //    Expression<Func<string, bool>> where1 = s => s.StartsWith("a");
            //    Expression<Func<string, bool>> where2 = s => s.Length > 10;
            //    Func<string, bool> func = where1.And(where2)
            //        //.AndIf(!string.IsNullOrEmpty(name), s => s == name)
            //        .Compile(); // And和AndIf可供选择，满足条件再执行And
            //    bool b = func("abcd12345678");//true

            //    Expression<Func<string, bool>> where3 = s => s.StartsWith("a");
            //    Expression<Func<string, bool>> where4 = s => s.Length > 10;
            //    Func<string, bool> func2 = where3
            //        .Or(where4)
            //        //.OrIf(!string.IsNullOrEmpty(name), s => s == name)
            //        .Compile(); // Or和OrIf可供选择，满足条件再执行Or
            //    bool b2 = func2("abc");// true

            //    List<LinqTest> queryable = new List<LinqTest>() {
            //new LinqTest(){ Name="A",Age=1},
            //new LinqTest(){ Name="B",Age=2}
            //};
            //    string name = "B";
            //    var linq = queryable.WhereIf(!string.IsNullOrEmpty(name), e => e.Name == name);
            //    //.WhereIf(() => age.HasValue, e => e.Age >= age); // IQueryable的WhereIf扩展函数，满足条件再执行Where
        }

        #endregion

        #region 模版引擎
        {
            var tmp = new Template("{{name}}，你好！");
            tmp.Set("name", "万金油");
            string s2 = tmp.Render();//万金油，你好！
        }

        {
            var tmp = new Template("{{one}},{{two}},{{three}}");
            string s = tmp.Set("one", "1").Set("two", "2").Set("three", "3").Render();// 1,2,3
        }

        {
            var tmp = new Template("{{name}}，{{greet}}！");
            tmp.Set("name", "万金油");
            string s = tmp.Render();// throw 模版变量{{greet}}未被使用
        }

        #endregion

        #region Linq转Datatable
        {
            List<LinqTest> list = new List<LinqTest>()
            {
                new LinqTest()
                {
                    Name = "张三",
                    Age = 22
                },
                new LinqTest()
                {
                    Name = "李四",
                    Age = 21
                },
                new LinqTest()
                {
                    Name = "王五",
                    Age = 28
                }
            };
            DataTable table = list.Select(c => new { 姓名 = c.Name, 年龄 = c.Age }).ToDataTable();// 将自动填充列姓名和年龄
            DataTable table2 = list.ToDataTable();// 集合转datatable
            Console.WriteLine(table2.HasRows());    // 检查DataTable 是否有数据行
            List<LinqTest> listTest = table2.ToList<LinqTest>();  // datatable转集合
            List<string> nameList = new List<string>() { "A", "B" };
            DataTable table3 = nameList.CreateTable();  // 创建datatable
            DataTable table4 = table.CreateTable("A,B,C");  // 创建datatable
            DataRow[] GetDataRowArray = table2.Rows.GetDataRowArray();  // DataRowCollection转换成的DataRow数组
            foreach (DataRow item in GetDataRowArray)
            {
                Console.WriteLine(item[0]);
            }
            DataTable table5 = GetDataRowArray.GetTableFromRows();  // 将DataRow数组转换成DataTable

        }
        #endregion

        #region 文件压缩解压(报错 未解决)
        {
            //HttpClient httpClient = new HttpClient();
            //ISevenZipCompressor _sevenZipCompressor = new SevenZipCompressor(httpClient);
            //var ms = _sevenZipCompressor.ZipStream(new List<string>()
            //{
            //    @"I:\downFIleWin10\WindowsInstallerCleanUP_V5.5_XiTongZhiJia\使用说明.txt",
            //    "http://118.178.227.145:8022//Uploads/2023/202301/20230104/20230104024659148380.jpg",
            //});//压缩成内存流

            //_sevenZipCompressor.Zip(new List<string>()
            //{
            //    @"D:\1.txt",
            //    "http://ww3.sinaimg.cn/large/87c01ec7gy1fsq6rywto2j20je0d3td0.jpg",
            //}, "zip");//压缩成zip
            //_sevenZipCompressor.UnRar(@"D:\Download\test.rar", @"D:\Download\");//解压rar
            //_sevenZipCompressor.Decompress(@"D:\Download\test.tar", @"D:\Download\");//自动识别解压压缩包
            //_sevenZipCompressor.Decompress(@"D:\Download\test.7z", @"D:\Download\");
        }
        #endregion

        #region 简易日志组件
        {
            LogManager.LogDirectory = AppDomain.CurrentDomain.BaseDirectory + "/logs";
            LogManager.Event += info =>
            {
                //todo:注册一些事件操作
            };
            LogManager.Info("记录一次消息");
            LogManager.Error(new Exception("异常消息"));
            LogManager.Debug("调试消息");
            LogManager.Fatal(new Exception("失败消息"));
        }
        #endregion

        #region FTP客户端(报错：创建Uri的时候，拼接请求地址又问题)
        {
            //FtpClient ftpClient = FtpClient.GetAnonymousClient("118.178.227.145");//创建一个匿名访问的客户端
            //FtpClient ftpClient = FtpClient.GetClient("118.178.227.145", "*****", "*****");// 创建一个带用户名密码的客户端
            //ftpClient.Download("/狗子的文件/1.txt", "D:\\1.txt",true);// 下载文件
            //ftpClient.Delete("/狗子的文件/1.txt");// 删除文件
            //ftpClient.UploadFile("/狗子的文件/2.txt", "D:\\1.txt", (sum, progress) =>
            //{
            //    Console.WriteLine("已上传：" + progress * 1.0 / sum);
            //});//上传文件并检测进度
            //List<string> files = ftpClient.GetFiles("/");//列出ftp服务端文件列表
        }
        #endregion

        #region 多线程后台下载
        {
            //// 多线程下载管理器
            //var mtd = new MultiThreadDownloader("https://download.filezilla.cn/client/windows/FileZilla_3.62.2_win32-setup.exe", Environment.GetEnvironmentVariable("temp"), "D:\\KeyShot_Pro_7.3.37.7z", 8);
            //// 配置请求头
            //mtd.Configure(req =>
            //{
            //    req.Referer = "https://masuit.com";
            //    req.Headers.Add("Origin", "https://baidu.com");
            //});
            //// 下载进度
            //mtd.TotalProgressChanged += (sender, e) =>
            //{
            //    var downloader = sender as MultiThreadDownloader;
            //    Console.WriteLine("下载进度：" + downloader?.TotalProgress + "%" + ";time:" + DateTime.Now);
            //    Console.WriteLine("下载速度：" + downloader?.TotalSpeedInBytes / 1024 / 1024 + "MBps");
            //};
            //// 文件合并事件
            //mtd.FileMergeProgressChanged += (sender, e) =>
            //{
            //    Console.WriteLine("下载完成");
            //};
            //// 文件合并完事件
            //mtd.FileMergedComplete += (sender, e) =>
            //{
            //    Console.WriteLine("文件合并完成");
            //};
            //mtd.Start();//开始下载
            //            //mtd.Pause(); // 暂停下载
            //            //mtd.Resume(); // 继续下载
        }
        #endregion

        #region 加密解密/hash
        {
            //var enc = "123456".MDString();// MD5
            //var enc2 = "123456".MDString("abc");// MD5加盐
            //var enc3 = "123456".MDString2();// MD5两次
            //var enc4 = "123456".MDString2("abc");// MD5两次加盐
            //var enc5 = "123456".MDString3();// MD5三次
            //var enc6 = "123456".MDString3("abc");// MD5三次加盐

            ////string aes = "123456".AESEncrypt();// AES加密为密文
            ////string s = aes.AESDecrypt(); //AES解密为明文
            //string aes7 = "123456".AESEncrypt("abc");// AES密钥加密为密文
            //string s8 = aes7.AESDecrypt("abc"); //AES密钥解密为明文

            ////string enc9 = "123456".DesEncrypt();// DES加密为密文
            ////string s10 = enc.DesDecrypt(); //DES解密为明文
            //string enc11 = "123456".DesEncrypt("abcdefgh");// DES密钥加密为密文
            //string s12 = enc11.DesDecrypt("abcdefgh"); //DES密钥解密为明文

            //RsaKey rsaKey = RsaCrypt.GenerateRsaKeys();// 生成RSA密钥对
            //string encrypt = "123456".RSAEncrypt(rsaKey.PublicKey);// 公钥加密
            //string s13 = encrypt.RSADecrypt(rsaKey.PrivateKey);// 私钥解密

            //string s14 = "123".Crc32();// 生成crc32摘要
            //string s15 = "123".Crc64();// 生成crc64摘要
            //string s16 = "123".SHA256();// 生成SHA256摘要

            //string pub = "hello,world!";
            //string hidden = "ldqk";
            //var str = pub.InjectZeroWidthString(hidden); // 扩展函数调用：将"ldqk"以零宽字符串的方式隐藏在"hello,world!"中
            //var str17 = ZeroWidthCodec.Encrypt(pub, hidden); // 类调用：将"ldqk"以零宽字符串的方式隐藏在"hello,world!"中
            //var dec = str.DecodeZeroWidthString(); // 扩展函数调用：将包含零宽字符串的密文解密出隐藏字符串"ldqk"
            //var dec18 = ZeroWidthCodec.Decrypt(str); // 类调用：将包含零宽字符串的密文解密出隐藏字符串"ldqk"
            //var enc19 = hidden.EncodeToZeroWidthText(); // 扩展函数调用：将字符串编码成零宽字符串
            //var enc20 = ZeroWidthCodec.Encode(pub); // 类调用：将字符串编码成零宽字符串
        }
        #endregion

        #region DateTime扩展
        {
            double milliseconds = DateTime.Now.GetTotalMilliseconds();// 获取毫秒级时间戳
            double microseconds = DateTime.Now.GetTotalMicroseconds();// 获取微秒级时间戳
            double nanoseconds = DateTime.Now.GetTotalNanoseconds();// 获取纳秒级时间戳
            double seconds = DateTime.Now.GetTotalSeconds();// 获取秒级时间戳
            double minutes = DateTime.Now.GetTotalMinutes();// 获取分钟级时间戳
        }
        #endregion

        #region IP地址和URL
        {
            bool inRange = "192.168.2.2".IpAddressInRange("192.168.1.1", "192.168.3.255");// 判断IP地址是否在这个地址段里
            bool isPrivateIp = "172.16.23.25".IsPrivateIP();// 判断是否是私有地址
            bool isExternalAddress = "http://baidu.com".IsExternalAddress();// 判断是否是外网的URL
        }
        #endregion

        #region 元素去重
        {
            var list = new List<MyClass>()
            {
                new MyClass()
                {
                    Email = "1@1.cn"
                },
                new MyClass()
                {
                    Email = "1@1.cn"
                },
                new MyClass()
                {
                    Email = "1@1.cn"
                }
            };
            List<MyClass> classes = list.DistinctBy(c => c.Email).ToList();
            Console.WriteLine(classes.Count == 1);//True
        }
        #endregion

        #region 枚举扩展
        {
            Dictionary<int, string> dic1 = typeof(MyEnum).GetDictionary();// 获取枚举值和字符串表示的字典映射
            Dictionary<string, int> dic2 = typeof(MyEnum).GetDescriptionAndValue();// 获取字符串表示和枚举值的字典映射
            string desc = MyEnum.Read.GetDescription();// 获取Description标签
            string display = MyEnum.Read.GetDisplay();// 获取Display标签的Name属性
            var value = typeof(MyEnum).GetValue("Read");// 获取字符串表示值对应的枚举值
            string enumString = 0.ToEnumString(typeof(MyEnum));// 获取枚举值对应的字符串表示
        }
        #endregion

        #region 定长队列和ConcurrentHashSet实现
        // 如果是.NET5及以上，推荐使用框架自带的Channel实现该功能
        {
            LimitedQueue<string> queue = new LimitedQueue<string>(32);// 声明一个容量为32个元素的定长队列
            ConcurrentLimitedQueue<string> queue2 = new ConcurrentLimitedQueue<string>(32);// 声明一个容量为32个元素的线程安全的定长队列

            var set = new ConcurrentHashSet<string>(); // 用法和hashset保持一致
        }
        #endregion

        #region 反射操作
        {
            MyClass myClass = new MyClass();
            PropertyInfo[] properties = myClass.GetProperties();// 获取属性列表
            myClass.SetProperty("Email", "1@1.cn");//给对象设置值
            myClass.DeepClone(); // 对象深拷贝，带嵌套层级的
        }
        #endregion

        #region 图像的简单处理
        {
            ////ImageUtilities.CompressImage(@"F:\src\1.jpg", @"F:\dest\2.jpg");//无损压缩图片 .net4.5才能使用

            //"base64".SaveDataUriAsImageFile();// 将Base64编码转换成图片

            //// 图像相似度对比
            //var hasher = new ImageHasher();
            //var hash1 = hasher.DifferenceHash256("图片1"); // 使用差分哈希算法计算图像的256位哈希
            //var hash2 = hasher.DifferenceHash256("图片2"); // 使用差分哈希算法计算图像的256位哈希
            ////var hash1 = hasher.AverageHash64("图片1"); // 使用平均值算法计算图像的64位哈希
            ////var hash2 = hasher.AverageHash64("图片2"); // 使用平均值算法计算图像的64位哈希
            ////var hash1 = hasher.DctHash("图片1"); // 使用DCT算法计算图像的64位哈希
            ////var hash2 = hasher.DctHash("图片2"); // 使用DCT算法计算图像的64位哈希
            ////var hash1 = hasher.MedianHash64("图片1"); // 使用中值算法计算给定图像的64位哈希
            ////var hash2 = hasher.MedianHash64("图片2"); // 使用中值算法计算给定图像的64位哈希
            //var sim = ImageHasher.Compare(hash1, hash2); // 图片的相似度，范围：[0,1]

        }
        #endregion

        #region 随机数
        {
            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random();
                Console.WriteLine(rnd.StrictNext());//产生真随机数
            }
            Console.WriteLine("*************************");
            for (int i = 0; i < 100; i++)
            {
                Random rnd = new Random();
                Console.WriteLine(rnd.NextGauss(20, 5));//产生正态高斯分布的随机数
            }
            Console.WriteLine("*************************");
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine(new NumberFormater(62).ToString(new Random().Next(100000, int.MaxValue)));//生成随机字符串
            }
            Console.WriteLine("*************************");
        }
        #endregion

        #region 权重筛选功能
        {
            List<WeightedItem<string>> data = new List<WeightedItem<string>>()
            {
                 new WeightedItem<string>("A", 1),
                 new WeightedItem<string>("B", 3),
                 new WeightedItem<string>("C", 4),
                 new WeightedItem<string>("D", 4),
            };
            string item = data.WeightedItem();//按权重选出1个元素
            List<string> list = data.WeightedItems(2);//按权重选出2个元素

            WeightedSelector<string> selector = new WeightedSelector<string>(new List<WeightedItem<string>>()
            {
                new WeightedItem<string>("A", 1),
                new WeightedItem<string>("B", 3),
                new WeightedItem<string>("C", 4),
                new WeightedItem<string>("D", 4),
            });
            string item2 = selector.Select();//按权重选出1个元素
            List<string> list2 = selector.SelectMultiple(3);//按权重选出3个元素
        }
        #endregion

        #region 敏感信息掩码
        {
            "13123456789".Mask(); // 131****5678
            "admin@masuit.com".MaskEmail(); // a****n@masuit.com
        }
        #endregion

        #region 集合扩展
        {
            List<string> list = new List<string>()
            {
                "1","3","3","3"
            };

            List<LinqTest> list2 = new List<LinqTest>(){
                new LinqTest(){Name="aa",Age=11},
                new LinqTest(){Name="bb",Age=12},
                new LinqTest(){Name="cc",Age=13},
            };
            list.AddRangeIf(s => s.Length > 1, "1", "11"); // 将被添加元素中的长度大于1的元素添加到list
            list.AddRangeIfNotContains("1", "11"); // 将被添加元素中不包含的元素添加到list
            list.RemoveWhere(s => s.Length > 1); // 将集合中长度小于1的元素移除
            list.InsertAfter(0, "2"); // 在第一个元素之后插入
            list.InsertAfter(s => s == "1", "4"); // 在元素"1"后插入
            var dic = list.ToDictionarySafety(s => s); // 安全的转换成字典类型，当键重复时只添加一个键
            var dic2 = list.ToConcurrentDictionary(s => s); // 转换成并发字典类型，当键重复时只添加一个键
            var dic3 = list.ToDictionarySafety(s => s, s => s.GetHashCode()); // 安全的转换成字典类型，当键重复时只添加一个键
            dic.AddOrUpdate(new Dictionary<string, string>() { ["6"] = "6" });  // 添加或更新键值对
            dic3.AddOrUpdate(new Dictionary<string, int>()
            {
                ["5"] = 5,
                ["55"] = 555
            }); // 批量添加或更新键值对
            dic3.AddOrUpdate("5", 6, (s, i) => 66); // 如果是添加，则值为6，若更新则值为66
            dic3.AddOrUpdate("5", 6, 666); // 如果是添加，则值为6，若更新则值为666
            dic3.GetOrAdd("7", 77); // 字典获取或添加元素
            dic3.GetOrAdd("7", () => 777); // 字典获取或添加元素
            dic3.AsConcurrentDictionary(); // 普通字典转换成并发字典集合

            DataTable table = list2.ToDataTable(); // 转换成DataTable类型
            DataTable dt = table.AddIdentityColumn(); //给DataTable增加一个自增列
            bool isHasRows = table.HasRows(); // 检查DataTable 是否有数据行
            List<LinqTest> list3 = table.ToList<LinqTest>(); // datatable转List
            HashSet<string> set = list2.ToHashSet(s => s.Name);// 转HashSet
            SyncSelect();

            string s = list.Join(",");//将字符串集合连接成逗号分隔的单字符串
            var max = list.MaxOrDefault(); // 取最大值，当集合为空的时候不会报错
            var max2 = list.MaxOrDefault(s => s.Length > 0); // 取最大值，当集合为空的时候不会报错
            var max3 = list.MaxOrDefault(s => s.Length > 123, default); // 取最大值，当集合为空的时候不会报错
            var max4 = list.MinOrDefault(); // 取最小值，当集合为空的时候不会报错
            var max5 = list.MinOrDefault(s => s.Length > 1); // 取最小值，当集合为空的时候不会报错
            var max6 = list.MinOrDefault(s => s.Length > 123, default); // 取最小值，当集合为空的时候不会报错
            var stdDev = list.Select(s => s.ConvertTo<int>()).StandardDeviation(); // 求标准差

            var pages = list2.ToPagedList(1, 2); // 分页查询

            var nums = Enumerable.Range(1, 10).ExceptBy(Enumerable.Range(5, 10), i => i); // 按字段取差集
            var nums8 = Masuit.Tools.IEnumerableExtensions.IntersectBy(Enumerable.Range(1, 10), Enumerable.Range(5, 10), i => i); // 按字段取交集
            var nums9 = Masuit.Tools.IEnumerableExtensions.SequenceEqual<int>(Enumerable.Range(1, 10), Enumerable.Range(5, 10), (i, x) => i == x); // 判断序列相等
            var nums10 = Enumerable.Range(1, 10).OrderByRandom(); // 随机排序

            // 多个集合取交集
            var list11 = new List<List<LinqTest>>(){
                new List<LinqTest>(){
                    new LinqTest(){Name="aa",Age=11},
                    new LinqTest(){Name="bb",Age=12},
                    new LinqTest(){Name="cc",Age=13},
                },
                new List<LinqTest>(){
                    new LinqTest(){Name="bb",Age=12},
                    new LinqTest(){Name="cc",Age=13},
                    new LinqTest(){Name="dd",Age=14},
                },
                new List<LinqTest>(){
                    new LinqTest(){Name="cc",Age=13},
                    new LinqTest(){Name="dd",Age=14},
                    new LinqTest(){Name="ee",Age=15},
                },
            };
            var sect = list11.IntersectAll(m => m.Name); // new LinqTest(){Name="cc",Age=13}

            var list12 = new List<List<int>>(){
                new(){1,2,3},
                new(){2,3,4},
                new(){3,4,5}
            };
            var sect13 = list12.IntersectAll();// [3]

            // 集合元素改变其索引位置
            list.ChangeIndex("1", 3); // 将元素item的索引位置变为第3个
            list2.ChangeIndex(t => t.Name == "aa", 2); // 将id为123的元素的索引位置变为第2个
        }
        #endregion

        #region Mime类型
        {
            MimeMapper mimeMapper = new MimeMapper();
            string ext = mimeMapper.GetExtensionFromMime("image/jpeg"); // .jpg
            string mime = mimeMapper.GetMimeFromExtension(".jpg"); // image/jpeg
        }
        #endregion

        #region 日期时间扩展
        {
            var indate = DateTime.Parse("2020-8-3").In(DateTime.Parse("2020-8-2"), DateTime.Parse("2020-8-4"));// 判断时间是是否在区间内
            DateTime time = "2021-1-1 8:00:00".ToDateTime(); //字符串转DateTime

            //时间段计算工具
            DateTimeRange range = new DateTimeRange(DateTime.Parse("2020-8-3"), DateTime.Parse("2020-8-5"));
            range.Union(DateTime.Parse("2020-8-4"), DateTime.Parse("2020-8-6")); //连接两个时间段，结果：2020-8-3~2020-8-6
            range.In(DateTime.Parse("2020-8-3"), DateTime.Parse("2020-8-6"));//判断是否在某个时间段内，true
            var (intersected, range2) = range.Intersect(DateTime.Parse("2020-8-4"), DateTime.Parse("2020-8-6"));//两个时间段是否相交，(true,2020-8-3~2020-8-4)
            range.Contains(DateTime.Parse("2020-8-3"), DateTime.Parse("2020-8-4"));//判断是否包含某个时间段，true
            var items = (DateTime.Now - new DateTime(2020, 10, 08)).Days;
        }
        #endregion

        #region 流相关
        {
            //Stream stream = new MemoryStream();
            //stream.SaveAsMemoryStream(); // 任意流转换成内存流
            //stream.ToArray(); // 任意流转换成二进制数组
            ////stream.ToArrayAsync(); // 任意流转换成二进制数组
            //stream.ShuffleCode(); // 流洗码，在流的末端随即增加几个空字节，重要数据请谨慎使用，可能造成流损坏

            //// 池化内存流，用法与MemorySteam保持一致
            //using var ms = new PooledMemoryStream();

            //// 大型内存流,最大可支持1TB内存数据，推荐当数据流大于2GB时使用，用法与MemorySteam保持一致
            //using var ms2 = new LargeMemoryStream();

            ////文件流快速复制
            //FileStream fs = new FileStream(@"D:\boot.vmdk", FileMode.OpenOrCreate, FileAccess.ReadWrite);
            //{
            //    fs.CopyToFile(@"D:\1.bak");//同步复制大文件
            //    //fs.CopyToFileAsync(@"D:\1.bak");//异步复制大文件
            //    Console.WriteLine(fs.GetFileMD5()); // 获取文件的MD5
            //    //string md5 = fs.GetFileMD5Async().Result;//异步获取文件的MD5
            //    string sha1 = fs.GetFileSha1();//异步获取文件的SHA1
            //}
            //MemoryStream memoryStream = new MemoryStream();
            //memoryStream.SaveFile("filename"); // 将内存流转储成文件
        }
        #endregion

        #region 数值转换
        {
            string result;
            1.23.ConvertTo<int>(); // 小数转int
            1.23.ConvertTo<decimal>(); // 小数转T基本类型
            bool b = 1.23.TryConvertTo<string>(out result); // 小数转T基本类型
            var num = 1.2345.ToDecimal(2); //转decimal并保留两位小数
        }
        #endregion

        #region INI配置文件操作(仅支持Windows)
        {
            //INIFile ini = new INIFile(@"D:\main.ini");
            //string section = "server";
            //string key = "ip";
            //string value = "127.0.0.1";
            //ini.IniWriteValue(section, key, value); // 写值
            //string iniVal = ini.IniReadValue(section, key); // 读值
            //ini.ClearSection(section); // 清空配置节
            //ini.ClearAllSection(); // 清空所有配置节
        }
        #endregion

        #region 雷达图计算引擎
        {
            //// 应用场景：计算两个多边形的相似度，用户画像之类的
            //var points = RadarChartEngine.ComputeIntersection(chart1, chart2); //获取两个多边形的相交区域
            //points.ComputeArea(); //计算多边形面积
        }
        #endregion

        #region 简单的Excel导出
        {
            //List<LinqTest> list = new List<LinqTest>(){
            //    new LinqTest(){Name="aa",Age=11},
            //    new LinqTest(){Name="bb",Age=12},
            //    new LinqTest(){Name="cc",Age=13},
            //};

            //var stream = list.Select(item => new {
            //    姓名 = item.Name,
            //    年龄 = item.Age,
            //    //item.Gender,
            //    //Avatar = Image.FromStream(filestream) //图片列
            //}).ToDataTable().ToExcel(); //自定义列名导出
            ////var stream2 = list.ToDataTable("Sheet1").ToExcel("文件密码");
            //var stream2 = list.ToDataTable("Sheet1").ToExcel("123456");

            //stream.SaveFile(@"D:\12.xlsx");
            //stream2.SaveFile(@"D:\13.xlsx");

            //一些约定规则：
            //1.图片列支持Stream、Bitmap、IEnumerable、IEnumerable、IDictionary<string, Stream>、IDictionary<string, MemoryStream>、IDictionary<string, Bitmap> 类型；
            //2.其中，如果是IDictionary类型的图片列，字典的键为图片超链接的完整url；
            //3.默认字段名作为列名导出；
            //4.若list是一个具体的强类型，默认会先查找每个字段的Description标记，若有Description标记，则取Description标记作为列名显示
            //5.ToExcel方法支持DataTable、List、Dictionary<string, DataTable> 类型的直接调用
        }
        #endregion

        #region EFCore实体对比功能
        {
            //// 取指定实体的变更
            //var changes = dbContext.GetChanges<Post>();//获取变更字段信息
            //var added = dbContext.GetAdded<Post>();//获取添加的实体字段信息
            //var removed = dbContext.GetRemoved<Post>();//获取被移除的实体字段信息  
            //var allchanges = dbContext.GetAllChanges<Post>();//获取增删改的实体字段信息  

            //// 获取所有实体的变更
            //var changes = dbContext.GetChanges();//获取变更字段信息
            //var added = dbContext.GetAdded();//获取添加的实体字段信息
            //var removed = dbContext.GetRemoved();//获取被移除的实体字段信息  
            //var allchanges = dbContext.GetAllChanges();//获取增删改的实体字段信息  
        }
        #endregion

        #region 任何类型支持链式调用
        {
            // a.Next(func1).Next(func2).Next(func3);
            "123".Next(s => s.ToInt32()).Next(x => x * 2).Next(x => Math.Log(x));
        }
        #endregion

        #region Newtonsoft.Json的只允许字段反序列化行为的契约解释器
        {
            //DeserializeOnlyContractResolver
            //该解释器针对类属性被DeserializeOnlyJsonPropertyAttribute标记的，在反序列化的时候生效，在序列化的时候忽略

            ClassDto ClassDto = new ClassDto()
            {
                MyProperty = "AA",
                Num = 1
            };

            string js = JsonConvert.SerializeObject(ClassDto);
            string js2 = JsonConvert.SerializeObject(ClassDto, new JsonSerializerSettings()
            {
                ContractResolver = new DeserializeOnlyContractResolver() // 配置使用DeserializeOnlyContractResolver解释器
            });

            // 如果是WebAPI全局使用：
            ////在Startup.ConfigureServices中
            //services.AddMvc().AddNewtonsoftJson(options =>
            //{
            //    var resolver = new DeserializeOnlyContractResolver();
            //    resolver.NamingStrategy = new CamelCaseNamingStrategy();
            //    options.SerializerSettings.ContractResolver = resolver;
            //});

            // FallbackJsonPropertyResolver
            // 该解释器针对某个属性设置多个别名，反序列化时支持多个别名key进行绑定，弥补官方JsonProperty别名属性只能设置单一别名的不足
            string js3 = JsonConvert.SerializeObject(new ClassDto2());
            string js4 = JsonConvert.SerializeObject(new ClassDto2(), new JsonSerializerSettings()
            {
                ContractResolver = new FallbackJsonPropertyResolver() // 配置使用FallbackJsonPropertyResolver解释器
            });
            var list= JsonConvert.DeserializeObject<ClassDto2>("{\"b\":\"abc\",\"num\":0}");
            var list2 = JsonConvert.DeserializeObject<ClassDto2>("{\"b\":\"abc\",\"num\":0}", new JsonSerializerSettings()
            {
                ContractResolver = new FallbackJsonPropertyResolver() // 配置使用FallbackJsonPropertyResolver解释器
            });

            // CompositeContractResolver
            // 该解释器是DeserializeOnlyContractResolver和FallbackJsonPropertyResolver的融合版
        }
        #endregion

        #region 字符串SimHash相似度算法
        {
            int dis = "12345678".HammingDistance("1234567");
            int dis2 = new SimHash("12345678").HammingDistance(new SimHash("1234567"));
        }
        #endregion

        #region 真实文件类型探测
        {
            //string filepath =@"D:\main.ini";
            //// 多种方式，任君调用
            ////var detector = new FileInfo(filepath).DetectFiletype();
            //var detector = File.OpenRead(filepath).DetectFiletype();
            ////var detector=FileSignatureDetector.DetectFiletype(filepath);
            //detector.Precondition;//基础文件类型
            //detector.Extension;//真实扩展名
            //detector.MimeType;//MimeType
            //detector.FormatCategories;//格式类别
        }
        #endregion

        Console.WriteLine("结束");
        Console.ReadLine();
    }

    public static string GetTestString()
    {
        return "123";
    }

    #region 异步Select，带索引编号
    public static async void SyncSelect()
    {
        CancellationTokenSource cts = new CancellationTokenSource(100); //取消口令

        List<string> list = new List<string>()
        {
            "1","3","3","3"
        };

        await list.ForeachAsync(async i =>
        {
            await Task.Delay(100);
            Console.WriteLine(i);
        }, cts.Token); // 异步foreach

        await list.ForAsync(async (item, index) =>
        {
            await Task.Delay(100);
            Console.WriteLine(item + "_" + index);
        }, cts.Token); // 异步for，带索引编号
        var selectasync = await list.SelectAsync(async i =>
         {
             await Task.Delay(100);
             Console.WriteLine(i);
             return i.ToInt32() * 10;
         }); // 异步Select

        Console.WriteLine(selectasync.ToJsonString());
        await list.SelectAsync(async (item, index) =>
        {
            await Task.Delay(100);
            return item.ToInt32() * 10;
        }); // 异步Select，带索引编号
    }

    #endregion



    #region Newtonsoft.Json的只允许字段反序列化行为的契约解释器
    //DeserializeOnlyContractResolver
    //该解释器针对类属性被DeserializeOnlyJsonPropertyAttribute标记的，在反序列化的时候生效，在序列化的时候忽略
    public class ClassDto
    {
        [DeserializeOnlyJsonProperty]
        public string MyProperty { get; set; }

        public int Num { get; set; }
    }

    // FallbackJsonPropertyResolver
    // 该解释器针对某个属性设置多个别名，反序列化时支持多个别名key进行绑定，弥补官方JsonProperty别名属性只能设置单一别名的不足
    public class ClassDto2
    {
        [FallbackJsonProperty("MyProperty", "a", "b")]
        public string MyProperty { get; set; }

        public int Num { get; set; }
    }
    #endregion

    public class LinqTest
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    #region 实体校验

    public class MyClass
    {
        [IsEmail] //可在appsetting.json中添加EmailDomainWhiteList配置邮箱域名白名单，逗号分隔
        public string Email { get; set; }

        [IsPhone]
        public string PhoneNumber { get; set; }

        [IsIPAddress]
        public string IP { get; set; }

        [MinValue(0, ErrorMessage = "年龄最小为0岁"), MaxValue(100, ErrorMessage = "年龄最大100岁")]
        public int Age { get; set; }

        [ComplexPassword]//密码复杂度校验
        public string Password { get; set; }

        [EnumOf(typeof(MyEnum))] // 检测是否是有效枚举值
        public MyEnum MyEnum { get; set; }

        [MinItemsCount(1)] // 检测集合元素最少1个
        public List<string> Strs { get; set; }
    }
    #endregion

    #region 枚举扩展
    public enum MyEnum
    {
        [Display(Name = "读")]
        [Description("读")]
        Read,

        [Display(Name = "写")]
        [Description("写")]
        Write
    }
    #endregion
}