using System;
using System.Reflection.Emit;
using static System.Console;

namespace SolidPrinciple
{
    public class Program
    {
        #region Definition
        //SOLID is an acronym for five design principles that help developers create more maintainable,
        //understandable, and flexible software.
        //These principles promote better software design and improve scalability and ease of testing.
        #endregion
        #region 1- Single Responsibility Principle (SRP)
        //A class should have only one reason to change, meaning it should have one responsibility.
        #region Example 1

        public class NotificationService
        {
            public  enum enNotificationType { Email, SMS, Fax}

            public void SendNotification(string to, string Message, enNotificationType notificationType)
            {
                if(notificationType == enNotificationType.Email)
                {
                   clsEmail.SendEmail(to, Message, clsEmail.enEmailType.Gmail);
                }
                else if(notificationType == enNotificationType.SMS)
                {
                   clsSMS.SendSMS(to, Message, clsSMS.enPhoneType.Huawi);
                }
                else if( notificationType == enNotificationType.Fax)
                {
                   clsFax.SendFax(to, Message);
                }
            }

        }
        public class clsEmail
        {
            public enum enEmailType { Gmail, Yahho}

            private static string GetEmailType(enEmailType emailType)
            {
                if (emailType == enEmailType.Gmail)
                    return "Gmail";
                else
                    return "Yahho";
            }

            public static void SendEmail(string to, string Message, enEmailType emailType)
            {
                Console.WriteLine($"\nSend email:\nMessage: {Message},\nTo: {to}[{GetEmailType(emailType)}],");
            }
        }
        public class clsSMS
        {
            public enum enPhoneType { Huawi, Samsunge, IPhone}

            private static string GetPhoneType(enPhoneType phoneType)
            {
                if (phoneType == enPhoneType.Huawi)
                    return "Huawi";
                else if (phoneType == enPhoneType.Samsunge)
                    return "Samsunge";
                else
                    return "IPhone";
            }

            public static void SendSMS(string to, string Message, enPhoneType phoneType)
            {
                Console.WriteLine($"\nSend SMS:\nMessage: {Message},\nTo: {to}[{GetPhoneType(phoneType)}].");
            }
        }
        public class clsFax
        {
            public static void SendFax(string to, string Message)
            {
                Console.WriteLine($"\nSend Fax:\nMessage: {Message},\nTo: {to}.");
            }
        }

        #endregion
        #region Example 2

        public class LoggingService
        {
            public enum enLogType { File, EventLog, Database}

            public void Log(string Message,  enLogType logType)
            {
                if(logType == enLogType.File)
                {
                    clsFile.LogToFile(Message);
                }
                else if(logType == enLogType.EventLog)
                {
                    clsEventLog.LogToEventLog(Message); 
                }
                else if(logType == enLogType.Database)
                    { clsDatabase.LogToDatabase(Message); }

            }
        }
        public class clsFile
        {
            public static void LogToFile(string Message)
            {
                WriteLine($"This message: {Message} is saved in file.");
            }
        }
        public class clsEventLog
        {
            public static void LogToEventLog(string Message)
            {
                WriteLine($"This message: {Message} is saved in EventLog.");
            }
        }
        public class clsDatabase
        {
            public static void LogToDatabase(string Message)
            {
                WriteLine($"This message: {Message} is saved in Database.");
            }
        }

        #endregion
        #endregion
        #region 2- Open / Close Principle (OCP)
        //Software entities should be open for extension but closed for modification.

        #region Example 1

        public class NotificationService2
        {
            private INotification _Notification;

            public NotificationService2(INotification Notification )
            {
                _Notification = Notification;
            }

            public void SendNotification(string to, string Message)
            {
                _Notification.Send(to, Message);
            }

        }
        public interface INotification
        {
             void Send(string to, string Message);
        }
        public class EmailService : INotification
        {
            public enum enEmailType { Gmail, Yahho }

            private static string GetEmailType(enEmailType emailType)
            {
                if (emailType == enEmailType.Gmail)
                    return "Gmail";
                else
                    return "Yahho";
            }

            private void SendEmail(string to, string Message, enEmailType emailType)
            {
                Console.WriteLine($"\nSend email:\nMessage: {Message},\nTo: {to}[{GetEmailType(emailType)}],");
            }

            public void Send(string to, string Message)
            {
                SendEmail(to, Message, enEmailType.Yahho);
            }
        }
        public class SMSservice : INotification
        {
            public enum enPhoneType { Huawi, Samsunge, IPhone }

            private static string GetPhoneType(enPhoneType phoneType)
            {
                if (phoneType == enPhoneType.Huawi)
                    return "Huawi";
                else if (phoneType == enPhoneType.Samsunge)
                    return "Samsunge";
                else
                    return "IPhone";
            }

            public static void SendSMS(string to, string Message, enPhoneType phoneType)
            {
                Console.WriteLine($"\nSend SMS:\nMessage: {Message},\nTo: {to}[{GetPhoneType(phoneType)}].");
            }

            public void Send(string to, string Message)
            {
                SendSMS(to, Message, enPhoneType.IPhone);
            }
        }
        public class FaxService : INotification
        {
            public void Send(string to, string Message)
            {
                Console.WriteLine($"\nSend Fax:\nMessage: {Message},\nTo: {to}.");
            }
        }
        public class FacebookService : INotification
        {
            public void Send(string to, string Message)
            {
                WriteLine($"\nSend Facebook:\nMessage: {Message},\nTo: {to}.");
            }
        }

        #endregion
        #region Example 2

        public class LoggingService2
        {
            private ILog _log;

            public LoggingService2(ILog log)
            {
                _log = log;
            }


            public void Log(string Message)
            {
               _log.Log(Message);
            }
        }
        public interface ILog
        {
            void Log(string Message);
        }
        public class FileService: ILog
        {
            public void Log(string Message)
            {
                WriteLine($"This message: {Message} is saved in file.");
            }
        }
        public class EventLogService:ILog
        {
            public void Log(string Message)
            {
                WriteLine($"This message: {Message} is saved in EventLog.");
            }
        }
        public class DatabaseService:ILog
        {
            public void Log(string Message)
            {
                WriteLine($"This message: {Message} is saved in Database.");
            }
        }
        public class OracleService : ILog
        {
            public void Log(string Message)
            {
                WriteLine($"This message: {Message} is saved in Oracle.");
            }
        }

        #endregion
        #region Example 3

        public class Company
        {
            IHiring _hiring;

            public Company(IHiring hiring)
            {
                _hiring = hiring;
            }

            public void Hiring(string Name, DateTime DateOfBirth)
            {
                _hiring.Hiring(Name, DateOfBirth);
            }

        }
        public interface IHiring
        {
            void Hiring(string Name, DateTime DateOfBirth);
        }
        public class clsPerson : IHiring
        {
            public void Hiring(string Name, DateTime DateOfBirth)
            {
                WriteLine($"New Hiring From Person:\nName: {Name}, DateOfBirth: {DateOfBirth.ToString("G")})");
            }
        }
        public class clsStudent : IHiring
        {
            public void Hiring(string Name, DateTime DateOfBirth)
            {
                WriteLine($"New Hiring From Student:\nName: {Name}, DateOfBirth: {DateOfBirth.ToString("G")})");
            }
        }

        #endregion
        #region Example 4

        public class PaymentService
        {
            IPay _pay;

            public PaymentService(IPay pay)
                { _pay = pay; }

            public void Payment(float Fees, DateTime dateTime)
            {
                _pay.Payment(Fees, dateTime);
            }

        }
        public interface IPay
        {
            void Payment(float Fees,DateTime dateTime);
        }
        public class PayPal : IPay
        {
            public void Payment(float Fees, DateTime dt)
            {
                WriteLine($"This fees [{Fees} $] was Pay by [\"Paypal\"], At [{dt.ToString("g")}]");
            }
        }
        public class CreditCard : IPay
        {
            public void Payment(float Fees, DateTime dt)
            {
                WriteLine($"This fees [{Fees} $] was Pay by [\"CreaditCard\"], At [{dt.ToString("g")}]");
            }
        }
        public class BankTransfer : IPay
        {
            public void Payment(float Fees, DateTime dt)
            {
                WriteLine($"This fees [{Fees} $] was Pay by [\"BankTransfer\"], At [{dt.ToString("g")}]");
            }
        }
        public class BitCoin : IPay
        {
            public void Payment(float Fees, DateTime dt)
            {
                WriteLine($"This fees [{Fees} $] was Pay by [\"BitCoin\"], At [{dt.ToString("g")}]");
            }
        }

        #endregion
        #region Example 5

        public class Draw
        {
            IDraw _draw;
            public Draw(IDraw draw)
                { _draw = draw; }
            public void DrawShape(string NameOfShape)
            {
                _draw.Draw(NameOfShape);
            }
        }
        public interface IDraw
        {
            void Draw(string NameOfShape);
        }
        public class clsSquare : IDraw
        {
            public void Draw(string NameOfShape)
            {
                WriteLine(NameOfShape);
                WriteLine("┌─────────┐");
                WriteLine("│         │");
                WriteLine("│         │");
                WriteLine("│         │");
                WriteLine("└─────────┘");
                WriteLine();
            }
        }
        public class clsRectangle : IDraw
        {
            public void Draw(string NameOfShape)
            {
                Console.WriteLine(NameOfShape);
                Console.WriteLine("┌─────────────────┐");
                Console.WriteLine("│                 │");
                Console.WriteLine("│                 │");
                Console.WriteLine("└─────────────────┘");
                Console.WriteLine();
            }
        }
        public class clsTriangle : IDraw
        {
            public void Draw(string NameOfShape)
            {
                Console.WriteLine(NameOfShape);
                Console.WriteLine("      /\\");
                Console.WriteLine("     /  \\");
                Console.WriteLine("    /    \\");
                Console.WriteLine("   /______\\");
                Console.WriteLine();
            }
        }
        public class clsCircle : IDraw
        {
            public void Draw(string NameOfShape)
            {
                Console.WriteLine(NameOfShape);
                Console.WriteLine("    *********");
                Console.WriteLine("  *           *");
                Console.WriteLine(" *             *");
                Console.WriteLine(" *             *");
                Console.WriteLine("  *           *");
                Console.WriteLine("    *********");
                Console.WriteLine();
            }
        }
        public class clsDiamond : IDraw
        {
            public void Draw(string NameOfShape)
            {
                Console.WriteLine(NameOfShape);
                Console.WriteLine("     /\\");
                Console.WriteLine("    /  \\");
                Console.WriteLine("   /    \\");
                Console.WriteLine("   \\    /");
                Console.WriteLine("    \\  /");
                Console.WriteLine("     \\/");
                Console.WriteLine();
            }
        }

        #endregion

        #endregion
        #region 3- Liskov Substitution Principle (LSP)
        //Objects of a superclass should be replaceable with objects of a subclass without affecting program correctness.

        #region Example 1

        public class Bird
        {
            public virtual void Eat()
            {
                WriteLine("Bird Eating");
            }
        }
        public class FlyingBird : Bird 
        {
            public virtual void Fly()
            {
                WriteLine("Bird Flying");
            }
        }
        public class Eagle : FlyingBird
        {
            public override void Eat()
            {
                WriteLine("Eagle Eating");
            }
            public override void Fly()
            {
                WriteLine("Eagle Flying");
            }
        }
        public class Ostrich : Bird
        {
            public override void Eat()
            {
                WriteLine("Ostrich Eating");
            }
        }

        #endregion
        #region Example 2

        public class Vehicle
        {
            public virtual void Drive()
            {
                WriteLine("Driving...");
            }
        }
        public class EngineVehicle : Vehicle
        {
            public virtual void StartEngine()
            {
                WriteLine("Start Engine");
            }
        }
        public class Car : EngineVehicle
        {
            public override void Drive()
            {
                WriteLine("Car Driving...");
            }

            public override void StartEngine()
            {
                WriteLine("Car engine started...");
            }
        }
        public class Plane : EngineVehicle
        {
            public override void Drive()
            {
                WriteLine("Plane Driving...");
            }

            public override void StartEngine()
            {
                WriteLine("Plane engine started...");
            }
        }
        public class Bicycle : Vehicle
        {
            public override void Drive()
            {
                WriteLine("Bicycle is riding...");
            }
        }

        #endregion
        #region Example 3

        public class Plant
        {
            public virtual string NameOfPlant { get; set; }
            public virtual void Root() => WriteLine($"{NameOfPlant}`s root is growing.");
            public virtual void Peel(bool Thick) => WriteLine($"{NameOfPlant}`s peel is: {(Thick ? "thick" : "thin")}.");
            
        }
        public class StemPlant : Plant
        {
            public virtual void Trunk(int Size) => WriteLine($"{NameOfPlant}`s trunk is: {Size} cm.");
        }
        public class Apple : StemPlant
        {
            public override string NameOfPlant { get => "Apple"; set => base.NameOfPlant = "Apple"; }

        }
        public class Watermelon : Plant
        {
            public override string NameOfPlant { get => "Watermelon"; set => base.NameOfPlant = "Watermelon"; }
        }

        #endregion
        #region Example 4

        public class Person
        {
            public virtual string Name { set; get; }
            public virtual void Call() => WriteLine($"Calling to a {Name}");
            public virtual void SendEmail() => WriteLine($"Sending email to a {Name}");
           
        }
        public class VolunteerEmployee : Person
        {
            public override string Name { get => "volunteerEmployee"; set => base.Name = "volunteerEmployee"; }
        }
        public class Employee :Person
        {
            public override string Name { get => "Employee"; set => base.Name = "Employee"; }
            public virtual void PaySalary() => WriteLine($"Paying salary to a {Name}");
        }
        public class Manager : Employee
        {
            public override string Name { get => "Manager"; set => base.Name = "Manager"; }
        }
        public class Student : Person
        {
            public override string Name { get => "Student"; set => base.Name = "Student"; }
        }
        public class User : Person
        {
            public override string Name { get => "User"; set => base.Name = "User"; }
        }

        #endregion
        #endregion
        #region 4- Interface Segregation Principle (ISP)
        //Clients should not be forced to depend on interfaces they do not use.

        #region Example 1

        public interface IPrint
        {
            void Print(string Text);
        }
        public interface IScan
        {
            void Scan();
        }
        public interface IFax
        {
            void Fax();
        }
        public class BasicPrinter : IPrint
        {
            public void Print(string Text)
            {
                WriteLine($"{Text}, At{DateTime.Now.ToString("g")}");
            }
        }
        public class AdvancedPrinter : IPrint,IFax,IScan
        {
            public void Fax()
            {
                WriteLine("Faxing...");
            }
            public void Print(string Text)
            {
                WriteLine($"{Text}, At{DateTime.Now.ToString("g")}");
            }
            public void Scan()
            {
                WriteLine("Scaning");
            }
        }

        #endregion
        #region Example 2

        public interface ICreditCardPayment
        {
             void CreditCardPay(string To, int Amount);
        }
        public interface IPayPalPayment
        {
            void PayPalPay(string To, int Amount);
        }
        public interface IBitCoinPayment
        {
            void BitCoinPay(string To, int Amount);
        }
        public class CreditCardPayment : ICreditCardPayment
        {
            public void CreditCardPay(string To, int Amount)
            {
                WriteLine($"Pay With CreditCard to:{To}, Amount:{Amount}$");
            }
        }
        public class PayPalPayment : IPayPalPayment
        {
           public void PayPalPay(string To, int Amount)
            {
                WriteLine($"Pay With PayPal to:{To}, Amount:{Amount}$");
            }
        }
        public class BitCoinPayment : IBitCoinPayment
        {
          public  void BitCoinPay(string To, int Amount)
            {
                WriteLine($"Pay With BitCoin to:{To}, Amount:{Amount}$");
            }
        }
        public class AllPayment : ICreditCardPayment, IPayPalPayment, IBitCoinPayment
        {
            public void BitCoinPay(string To, int Amount)
            {
                WriteLine($"Pay With BitCoin to:{To}, Amount:{Amount}$");
            }
            public void CreditCardPay(string To, int Amount)
            {
                WriteLine($"Pay With CreditCard to:{To}, Amount:{Amount}$");
            }
            public void PayPalPay(string To, int Amount)
            {
                WriteLine($"Pay With PayPal to:{To}, Amount:{Amount}$");
            }
        }

        #endregion
        #region Example 3

        public interface ICallDevice
        {
            void Call(string NumberPhone);
        }
        public interface IPhotoDevice
        {
            void TakePhoto();
        }
        public interface IEmailDevice
        {
            void SendEmail(string Message,string Email);
        }
        public interface IGPSDevice
        {
            void UseGPS();
        }
        public interface ISmartPhone : ICallDevice, IPhotoDevice, IEmailDevice, IGPSDevice
        { }
        public class SmartPhone :ISmartPhone
        {
            public void Call(string NumberPhone)
            {
                WriteLine($"Calling to {NumberPhone}");
            }

            public void SendEmail(string Email)
            {
                WriteLine($"Sending to {Email}");
            }

            public void SendEmail(string Message, string Email)
            {
                WriteLine($"Send email to {Email}, Message: {Message}");
            }

            public void TakePhoto()
            {
                WriteLine("Taking a picture");
            }

            public void UseGPS()
            {
                WriteLine("Using GPS");
            }
        }
        public class Computer : IEmailDevice
        {
            public void SendEmail(string Message, string Email)
            {
                WriteLine($"Send email to {Email}, Message: {Message}");
            }
        }
        public class Camera : IPhotoDevice
        {
            public void TakePhoto()
            {
                WriteLine("Taking photo...");
            }
        }
        public class AdvancedCamera : IPhotoDevice, IEmailDevice
        {
            public void SendEmail(string Message, string Email)
            {
                WriteLine($"Sending Email: Message[{Message}], To:[{Email}]");
            }

          public  void TakePhoto()
            {
                WriteLine("Taking a picture");
            }
        }

        #endregion
        #region Example 4

        public enum enSize { Small=1, Medium, Big}
        public interface IBreakfast
        {
            /// <summary>
            /// For Prapering the egg plate.
            /// </summary>
            /// <param name="Price">The amount of plate</param>
            /// <param name="Size">The Size of plate</param>
            void EggPlate(int Price, enSize Size);

            /// <summary>
            /// Making a cup of tea.
            /// </summary>
            /// <param name="Price">The Price of cup</param>
            void MakeCupOfTea(int Price);
        }
        public interface ILunch
        {
            void MeatMeal(int Price);
            void FishMeal(int Price);
            void ChickenMeal(int Price);
        }
        public interface IDinner
        {
            /// <summary>
            /// Prapering a plate of kebab
            /// </summary>
            /// <param name="Price">The amount of plate</param>
            /// <param name="Size">The Size of plate</param>
            void KebabPlate(int Price, enSize Size);
        }

        public class Cafeteria : IBreakfast
        {
            public void EggPlate(int Price, enSize Size)
            {
                WriteLine($"New order for EggPlate:\nSize: {(Size == enSize.Small ? "Small" : (Size ==  enSize.Medium ? "Medium" : "Big"))},\nPrice: {Price}$.\n");
            }

            public void MakeCupOfTea(int Price)
            {
                WriteLine($"New order for MakeCupOfTea:\nPrice: {Price}$\n");
            }
        }
        public class ArabicRestaurant : ILunch
        {
            public void ChickenMeal(int Price)
            {
                WriteLine($"New order for ChickenMeal:\nPrice: {Price}$\n");
            }

            public void FishMeal(int Price)
            {
                WriteLine($"New order for FishMeal:\nPrice: {Price}$\n");
            }

            public void MeatMeal(int Price)
            {
                WriteLine($"New order for MeatMeal:\nPrice: {Price}$\n");
            }
        }
        public class ShamRestaurant : IDinner
        {
            public void KebabPlate(int Price, enSize Size)
            {
                WriteLine($"New order for KebabPlate:\nSize: {(Size ==  enSize.Small ? "Small" : (Size ==  enSize.Medium ? "Medium" : "Big"))},\nPrice: {Price}$.\n");
            }
        }

        #endregion
        #endregion
        #region 5- Dependency Inversion Principle (DIP)
       //High-level modules should not depend on low-level modules; both should depend on abstractions.

        #region Example 1

        public interface IReport
        {
            void Generate();
        }
        public class ReportGenerate
        {
            IReport _report;
            public ReportGenerate(IReport report) // Dependency Injection via constructor
            {
                _report = report;
            }

            public void Generate() => 
                _report.Generate();

        }
        public class PDFReport : IReport
        {
            public void Generate()
            {
                WriteLine("PDF report generated.");
            }
        }
        public class WordReport : IReport
        {
            public void Generate()
            {
                WriteLine("Word report generated.");
            }
        }
        public class ExcelReport : IReport
        {
            public void Generate()
            {
                WriteLine("Excel report generated.");
            }
        }

        #endregion

        #endregion

        static void Main(string[] args)
        {
            
        }
    }
}
