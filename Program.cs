using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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

        static void Choi()
        {
            string van_dau = "";
            bool luot_di = true;
            int x = 25, y = 17;
            int x_tam, y_tam;

            The Itsuka_Kotori = new The(100, 10, 10);
            The Tokisaki_Kurumi = new The(100, 10, 10);

            if (luot_di = true)
                van_dau = "Lượt của bạn!";
            else
                van_dau = "Lượt của máy!";

            while (true)
            {
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
                        Console.Clear();
                        if (y == 17)
                        {
                            KyNang(1);
                        }    
                            
                        if (y == 19)
                        {
                            KyNang(2);
                        }    
                            
                        if (y == 21)
                        {
                            KyNang(3);
                        }    
                           
                        if (y == 23)
                        {
                            KyNang(4);
                        }    
                           
                        Console.Clear();
                        break;

                    case ConsoleKey.Q:
                        return;
                        break;
                }

                Console.SetCursorPosition(x_tam, y_tam);
                Console.Write("  ");

            }

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
                        Console.WriteLine("Thẻ Hồi Máu: Hồi 20% máu tối đa.");
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

        static void KyNang(int id)
        {
            switch (id)
            {
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                    
                    break;
                case 4:
                    
                    break;
            }
        }
    }
}
