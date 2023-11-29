using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaifuCardsBattle
{

    public class The
    {
        private int mau;
        private int cong;
        private int thu;

        public int Mau { get => mau; set => mau = value; }
        public int Cong { get => cong; set => cong = value; }
        public int Thu { get => thu; set => thu = value; }

        public The()
        {
            this.mau = 0;
            this.cong = 0;
            this.thu = 0;
        }

        public The(int mau, int cong, int thu)
        {
            this.mau = mau;
            this.cong = cong;
            this.thu = thu;
        }


    }

    internal class Program
    {

        static void Main(string[] args)
        {
            menu();
        }

        static void menu()
        {
            int x = 5, y = 7;
            int x_tam, y_tam;

            while (true)
            {
                Console.SetCursorPosition(5, 5);
                Console.WriteLine("Waifu Cards Battle");
                Console.SetCursorPosition(7, 7);
                Console.WriteLine("Chơi");
                Console.SetCursorPosition(7, 9);
                Console.WriteLine("Giới Thiệu");
                Console.SetCursorPosition(7, 11);
                Console.WriteLine("Thoát");


                Console.SetCursorPosition(x, y);
                Console.Write("->");


                x_tam = x;
                y_tam = y;

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.W:
                        if (y >= 9)
                            y -= 2;
                        break;

                    case ConsoleKey.S:
                        if (y <= 9)
                            y += 2;
                        break;

                    case ConsoleKey.K:
                        Console.Clear();
                        if (y == 7)
                            Choi();
                        if (y == 9)
                            GioiThieu();
                        if (y == 11)
                            Thoat();
                        Console.Clear();
                        break;
                }

                Console.SetCursorPosition(x_tam, y_tam);
                Console.Write("  ");
            }
        }

        static void GioiThieu()
        {
            while (true)
            {
                Console.SetCursorPosition(5, 5);
                Console.WriteLine("Đây là game chiến đấu với các thẻ bài Waifu!");
                Console.SetCursorPosition(5, 5);

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.Q:
                        return;
                }
            }

        }

        static void Thoat()
        {
            Environment.Exit(0);
        }

        static The Itsuka_Kotori = new The(100, 10, 1);
        static The Tokisaki_Kurumi = new The(100, 10, 1);
        static bool giam_cong_nguoi_choi = false;
        static bool giam_cong_may = false;
        static void Choi()
        {
            string van_dau = "";
            bool luot_di = true;
            int x = 25, y = 17;
            int x_tam, y_tam;

            while (true)
            {
                if (luot_di == true)
                    van_dau = "Lượt của bạn!";
                else
                    van_dau = "Lượt của máy!";
                    
                Console.SetCursorPosition(25, 5);
                Console.WriteLine("Thẻ: Tokisaki Kurumi(DAL) Máu: "+ Tokisaki_Kurumi.Mau + " Công: "+ Tokisaki_Kurumi.Cong + " Thủ: "+ Tokisaki_Kurumi.Thu);

                Console.SetCursorPosition(25, 10);
                Console.WriteLine(van_dau);
                
                Console.SetCursorPosition(25, 15);
                Console.WriteLine("Thẻ: Itsuka Kotori(DAL) Máu: "+ Itsuka_Kotori.Mau + " Công: "+ Itsuka_Kotori.Cong + " Thủ: "+ Itsuka_Kotori.Thu);
                Console.SetCursorPosition(27, 17);
                Console.WriteLine("Tấn công");
                Console.SetCursorPosition(27, 19);
                Console.WriteLine("Hồi máu");
                Console.SetCursorPosition(27, 21);
                Console.WriteLine("Phòng thủ");
                Console.SetCursorPosition(27, 23);
                Console.WriteLine("Giảm sức mạnh");

                Console.SetCursorPosition(x, y);
                Console.Write("->");

                x_tam = x;
                y_tam = y;

                string van_dau_tam = van_dau;

                if (luot_di == true)
                {
                    ConsoleKeyInfo lua_chon = Console.ReadKey();
                    switch (lua_chon.Key)
                    {
                        case ConsoleKey.W:
                            if (y >= 19)
                                y -= 2;
                            break;

                        case ConsoleKey.S:
                            if (y <= 21)
                                y += 2;
                            break;

                        case ConsoleKey.P:
                            Console.Clear();
                            if (y == 17)
                                ThongTin(1);
                            if (y == 19)
                                ThongTin(2);
                            if (y == 21)
                                ThongTin(3);
                            if (y == 23)
                                ThongTin(4);
                            Console.Clear();
                            break;

                        case ConsoleKey.K:
                            if (y == 17)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng tấn công!";
                                KyNang(1, luot_di);
                            }

                            if (y == 19)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng hồi máu!";
                                KyNang(2, luot_di);
                            }

                            if (y == 21)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng phòng thủ!";
                                KyNang(3, luot_di);
                            }

                            if (y == 23)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng giảm sức mạnh!";
                                KyNang(4, luot_di);
                            }
                            luot_di = !luot_di;
                            Console.SetCursorPosition(25, 10);
                            for (int i = 0; i < van_dau_tam.Length; i++)
                                Console.WriteLine(" ");
                            Console.SetCursorPosition(25, 10);
                            Console.WriteLine(van_dau);
                            Thread.Sleep(1000);
                            if (giam_cong_nguoi_choi == true)
                            {
                                giam_cong_nguoi_choi = false;
                                Itsuka_Kotori.Cong *= 2;
                            }    
                               
                            Console.Clear();
                            break;

                        case ConsoleKey.Q:
                            return;
                    }
                }
                else
                {
                    Thread.Sleep(1000);

                    int id = AI();
                    if (id == 1)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng tấn công!";
                        KyNang(1, luot_di);
                    }

                    if (id == 2)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng hồi máu!";
                        KyNang(2, luot_di);
                    }

                    if (id == 3)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng phòng thủ!";
                        KyNang(3, luot_di);
                    }

                    if (id == 4)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng giảm sức mạnh!";
                        KyNang(4, luot_di);
                    }
                    luot_di = !luot_di;
                    Console.SetCursorPosition(25, 10);
                    for (int i = 0; i < van_dau_tam.Length; i++)
                        Console.WriteLine(" ");
                    Console.SetCursorPosition(25, 10);
                    Console.WriteLine(van_dau);
                    Thread.Sleep(1000);
                    if (giam_cong_may == true)
                    {
                        giam_cong_may = false;
                        Tokisaki_Kurumi.Cong *= 2;
                    }
                    Console.Clear();
                }
                    
                Console.SetCursorPosition(x_tam, y_tam);
                Console.Write("  ");

            }

        }

        static int AI()
        {
            Random rd = new Random();
            return rd.Next(1,4);
        }

        static void ThongTin(int id)
        {
            while(true)
            {
                Console.SetCursorPosition(5, 5);
                switch (id)
                {
                    case 1:
                        Console.WriteLine("Thẻ Tấn Công: Tấn công với sức tấn công bằng chỉ số công hiện tại.");
                        break;
                    case 2:
                        Console.WriteLine("Thẻ Hồi Máu: Hồi máu với 20% máu tối đa hiện tại.");
                        break;
                    case 3:
                        Console.WriteLine("Thẻ Phòng Thủ: Tăng phòng thủ hiện tại lên 1.");
                        break;
                    case 4:
                        Console.WriteLine("Thẻ Giảm Sức Mạnh: Giảm một nửa sức tấn công của đối phương ở lượt kế.");
                        break;
                }
                Console.SetCursorPosition(5, 5);

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.Q:
                        return;
                }
            }    
           
        }

        static void KyNang(int id, bool luot_di)
        {
            switch (id)
            {
                case 1:
                    if (luot_di == true)
                    {
                        int suc_tan_cong = Itsuka_Kotori.Cong - Tokisaki_Kurumi.Thu;
                        if(suc_tan_cong > 0)
                            Tokisaki_Kurumi.Mau -= suc_tan_cong;
                    }    
                       
                    else
                    {
                        int suc_tan_cong = Tokisaki_Kurumi.Cong - Itsuka_Kotori.Thu;
                        if (suc_tan_cong > 0)
                            Itsuka_Kotori.Mau -= Tokisaki_Kurumi.Cong - Itsuka_Kotori.Thu;
                    }    
                        
                    break;
                case 2:
                    if (luot_di == true)
                    {
                        Itsuka_Kotori.Mau += (Itsuka_Kotori.Mau * 20) / 100;
                        if (Itsuka_Kotori.Mau > 100)
                            Itsuka_Kotori.Mau = 100;
                    }    
                    else
                    {
                        Tokisaki_Kurumi.Mau += (Tokisaki_Kurumi.Mau * 20) / 100;
                        if (Tokisaki_Kurumi.Mau > 100)
                            Tokisaki_Kurumi.Mau = 100;
                    }    
                    break;
                case 3:
                    if (luot_di == true)
                        Itsuka_Kotori.Thu++;
                    else
                        Tokisaki_Kurumi.Thu++;
                    break;
                case 4:
                    if (luot_di == true)
                    {
                        Tokisaki_Kurumi.Cong -= Tokisaki_Kurumi.Cong / 2;
                        giam_cong_may = true;
                    }    
                       
                    else
                    {
                        Itsuka_Kotori.Cong -= Itsuka_Kotori.Cong / 2;
                        giam_cong_nguoi_choi = true;
                    }    
                        
                    break;
            }
        }


    }
}
