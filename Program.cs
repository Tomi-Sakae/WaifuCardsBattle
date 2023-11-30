﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaifuCardsBattle
{
    // Waifu Cards Battle Code viết bởi Tomi Sakae (https://github.com/Tomi-Sakae/WaifuCardsBattle)
    public class TheWaifu
    {
        private int mau;
        private int cong;
        private int thu;
        private string ten;
        private int id;
        private string ky_nang_toi_thuong;
        private string mo_ta_ky_nang_toi_thuong;

        public int Mau { get => mau; set => mau = value; }
        public int Cong { get => cong; set => cong = value; }
        public int Thu { get => thu; set => thu = value; }
        public string Ten { get => ten; set => ten = value; }
        public int Id { get => id; set => id = value; }
        public string Ky_nang_toi_thuong { get => ky_nang_toi_thuong; set => ky_nang_toi_thuong = value; }
        public string Mo_ta_ky_nang_toi_thuong { get => mo_ta_ky_nang_toi_thuong; set => mo_ta_ky_nang_toi_thuong = value; }
        public TheWaifu()
        {
            this.mau = 0;
            this.cong = 0;
            this.thu = 0;
            this.ten = "";
            this.id = 0;
            this.ky_nang_toi_thuong = "";
            this.mo_ta_ky_nang_toi_thuong = "";
        }

        public TheWaifu(int mau, int cong, int thu, string ten, int id, string ky_nang_toi_thuong, string mo_ta_ky_nang_toi_thuong)
        {
            this.mau = mau;
            this.cong = cong;
            this.thu = thu;
            this.ten = ten;
            this.id = id;
            this.ky_nang_toi_thuong = ky_nang_toi_thuong;
            this.mo_ta_ky_nang_toi_thuong = mo_ta_ky_nang_toi_thuong;
        }


    }

    public class TheKyNang
    {
        private int id;
        private string ten;
        private string mo_ta;

        public int Id { get => id; set => id = value; }
        public string Ten { get => ten; set => ten = value; }

        public string Mo_ta { get => mo_ta; set => mo_ta = value; }

        public TheKyNang()
        {
            this.id = 0;
            this.ten = "";
            this.mo_ta = "";
        }

        public TheKyNang(int id, string ten, string mo_ta)
        {
            this.id = id;
            this.ten = ten;
            this.mo_ta = mo_ta;
        }    
    }

    internal class Program
    {
        static void Reset()
        {
            random = new Random();
            rd = new Random();
            Itsuka_Kotori = new TheWaifu(100, 10, 1, "Itsuka Kotori", 1, "Pháo Hỏa Ngục", "Pháo Hỏa Ngục: Bắn một tia lửa có nhiệt lượng cực cao vào đối thủ gây 500% sát thương so với sức tấn công hiện tại.");
            Tokisaki_Kurumi = new TheWaifu(100, 10, 1, "Tokisaki Kurumi", 2, "Điều Chỉnh Thời Gian", "Điều Chỉnh Thời Gian: Ngưng động thời gian của đối thủ. Được phép hành động trong 5 lượt liên tiếp.");
            kiem_tra_ky_nangAI = true;
            thoi_gian_may = true;
            thoi_gian_nguoi_choi = true;
            dem_thoi_gian = 0;
            giam_cong_nguoi_choi = false;
            giam_cong_may = false;
            giam_thu_nguoi_choi = false;
            giam_thu_may = false;
            tang_cong_nguoi_choi = false;
            tang_cong_may = false;
        }

        static Random random = new Random();
        static Random rd = new Random();
        static List<int> NgauNhien(int minValue, int maxValue) // Hàm chọn số ngẫu nhiên trong khoảng với điều kiện không trùng nhau
        {
            List<int> numbers = new List<int>();

            for (int i = minValue; i < maxValue; i++)
            {
                int randomNumber;
                do
                {
                    randomNumber = random.Next(minValue, maxValue);
                } while (numbers.Contains(randomNumber));

                numbers.Add(randomNumber);
            }

            return numbers;
        }


        static TheWaifu Itsuka_Kotori = new TheWaifu(100, 10, 1, "Itsuka Kotori", 1, "Pháo Hỏa Ngục", "Pháo Hỏa Ngục: Bắn một tia lửa có nhiệt lượng cực cao vào đối thủ gây 500% sát thương so với sức tấn công hiện tại.");

        static public int PhaoHoaNguc(int cong, int thu, int mau)
        {
           
            if (((cong + ((cong * 500) / 100)) - thu) <= 0)
                return 0;
            else
                return mau - (cong + ((cong * 500) / 100));
        }

        static TheWaifu Tokisaki_Kurumi = new TheWaifu(100, 10, 1, "Tokisaki Kurumi", 2, "Điều Chỉnh Thời Gian", "Điều Chỉnh Thời Gian: Ngưng động thời gian của đối thủ. Được phép hành động trong 5 lượt liên tiếp.");

        static bool thoi_gian_may = true;
        static bool thoi_gian_nguoi_choi = true;
        static int dem_thoi_gian = 0;
        static public void DieuChinhThoiGian(int id)
        {
            switch(id)
            {
                case 1:
                    thoi_gian_may = false;
                    break;
                case 2:
                    thoi_gian_nguoi_choi = false;
                    break;
            }
            dem_thoi_gian = 5;
        }    

        static TheWaifu Nguoi = new TheWaifu();
        static TheWaifu AI = new TheWaifu();
        static int so_luong_waifu = 2;

        static TheKyNang KyNangTanCong = new TheKyNang(1, "Tấn Công", "Thẻ Tấn Công: Tấn công với sức tấn công bằng chỉ số công hiện tại.");
        static public int TanCong(int cong, int thu, int mau)
        {
            if ((cong - thu) <= 0)
                return 0;
            else
                return mau - (cong - thu);
        }

        static TheKyNang KyNangHoiMau = new TheKyNang(2, "Hồi Máu", "Thẻ Hồi Máu: Hồi máu với 20% máu hiện tại.");
        static public int HoiMau(int mau, int mau_toi_da)
        {
            if ((mau + ((mau * 20) / 100)) >= mau_toi_da)
                return mau_toi_da;
            else
                return mau + (mau* 20) / 100;
        }

        static TheKyNang KyNangPhongThu = new TheKyNang(3, "Phòng Thủ", "Thẻ Phòng Thủ: Tăng phòng thủ hiện tại lên 1.");
        static public int PhongThu(int thu)
        {
            return ++thu;
        }

        static TheKyNang KyNangTangCuong = new TheKyNang(4, "Tăng Cường", "Thẻ Tăng Cường: Tăng tấn công hiện tại lên 1.");
        static public int TangCuong(int cong)
        {
            return ++cong;
        }

        static TheKyNang KyNangGiamSucManh = new TheKyNang(5, "Giảm Sức Mạnh", "Thẻ Giảm Sức Mạnh: Giảm một nửa sức tấn công của đối thủ trong lượt kế. Khi kết thúc lượt nhân đôi sức tấn công hiện tại.");
        static public int GiamSucManh(int cong)
        {
            return cong / 2;
        }
        static bool giam_cong_nguoi_choi = false;
        static bool giam_cong_may = false;

        static TheKyNang KyNangHoiPhuc = new TheKyNang(6, "Hồi Phục", "Thẻ Hồi Phục: Hồi máu với 20% máu tối đa hiện tại.");
        static public int HoiPhuc(int mau, int mau_toi_da)
        {
            if ((mau + ((mau_toi_da * 20) / 100)) >= mau_toi_da)
                return mau_toi_da;
            else
                return mau + (mau_toi_da * 20) / 100;
        }

        static TheKyNang KyNangChucPhucCuaThienThan = new TheKyNang(7, "Chúc Phúc Của Thiên Thần", "Thẻ Chúc Phúc Của Thiên Thần: Tăng 10% máu tối đa.");
        static public int ChucPhucCuaThienThan(int mau_toi_da)
        {
            return mau_toi_da + (mau_toi_da * 10) / 100;
        }

        static TheKyNang KyNangTanCongChiMang = new TheKyNang(8, "Tấn Công Chí Mạng", "Thẻ Tấn Công Chí Mạng: Tấn công đối thủ với X2 sức tấn công.");
        static public int TanCongChiMang(int cong, int thu, int mau)
        {
            if ((cong*2 - thu) <= 0)
                return 0;
            else
                return mau - (cong*2 - thu);
        }

        static TheKyNang KyNangPhaGiap = new TheKyNang(9, "Phá Giáp", "Thẻ Phá Giáp: Làm chỉ số phòng thủ của đối thủ giảm về 0 ở lượt kế.");
        static public int PhaGiap()
        {
            return 0;
        }

        static bool giam_thu_nguoi_choi = false;
        static bool giam_thu_may = false;
        static int luu_thu_nguoi_choi;
        static int luu_thu_may;
        static int luu_dem_pha_giap_nguoi_choi;
        static int luu_dem_pha_giap_may;

        static TheKyNang KyNangSieuTangCuong = new TheKyNang(10, "Siêu Tăng Cường", "Thẻ Siêu Tăng Cường: X2 chỉ số tấn công hiện tại ở lượt kế.");
        static public int SieuTangCuong(int cong)
        {
            return cong*2;
        }

        static bool tang_cong_nguoi_choi = false;
        static bool tang_cong_may = false;
        static int luu_cong_nguoi_choi;
        static int luu_cong_may;
        static int luu_dem_sieu_tang_cuong_nguoi_choi;
        static int luu_dem_sieu_tang_cuong_may;

        static TheKyNang[] MangKyNang = new TheKyNang[100];
        static TheKyNang[] MangKyNangAI = new TheKyNang[100];
        static int so_luong_ky_nang = 10;


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


        static int nguoi_mau_toi_da;
        static int may_mau_toi_da;
        static bool kiem_tra_ky_nangAI = true;
        static int luu_id_nguoi;

        static TheKyNang[] LuuKyNang = new TheKyNang[100];
        static TheKyNang[] LuuKyNangAI = new TheKyNang[100];

        static TheKyNang[] DaDungKyNang = new TheKyNang[100];
        static TheKyNang[] DaDungKyNangAI = new TheKyNang[100];
        static void Choi()
        {
            string van_dau = "";
            bool luot_di = true;
            int x = 25, y = 17;
            int x_tam, y_tam;
            bool kiem_tra_ky_nang = true;
           
            int gia_tri_luot_di = rd.Next(1, 3);

            if (gia_tri_luot_di == 1)
                luot_di = true;
            else
                luot_di = false;

            List<int> numbersNguoi = NgauNhien(1, so_luong_waifu+1);

            int demNguoi = 0;
            foreach (int numberNguoi in numbersNguoi)
            {
                demNguoi++;
                switch (numberNguoi)
                {
                    case 1:
                        Nguoi = Itsuka_Kotori;
                        luu_id_nguoi = 1;
                        break;
                    case 2:
                        Nguoi = Tokisaki_Kurumi;
                        luu_id_nguoi = 2;
                        break;
                }
                if (demNguoi == 1)
                    break;
            }

            List<int> numbersNguoiAI = NgauNhien(1, so_luong_waifu+1);

            int demNguoiAI = 0;
            foreach (int numberNguoiAI in numbersNguoiAI)
            {
                demNguoiAI++;
                if (luu_id_nguoi != numberNguoiAI)
                {
                    switch (numberNguoiAI)
                    {
                        case 1:
                            AI = Itsuka_Kotori;
                            break;
                        case 2:
                            AI = Tokisaki_Kurumi;
                            break;
                    }
                    if (demNguoiAI == 1)
                        break;
                }
                else
                    demNguoiAI--;
               
            }

            List<int> numbers = NgauNhien(1, so_luong_ky_nang+1);

            int dem = 0;
            foreach (int number in numbers)
            {
                dem++;
                switch (number)
                {
                    case 1:
                        if(dem <= 4)
                            MangKyNang[dem] = KyNangTanCong;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCong;
                        break;
                    case 2:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangHoiMau;
                        else
                            LuuKyNang[dem - 4] = KyNangHoiMau;
                        break;
                    case 3:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangPhongThu;
                        else
                            LuuKyNang[dem - 4] = KyNangPhongThu;
                        break;
                    case 4:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTangCuong;
                        else
                            LuuKyNang[dem - 4] = KyNangTangCuong;
                        break;
                    case 5:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangGiamSucManh;
                        else
                            LuuKyNang[dem - 4] = KyNangGiamSucManh;
                        break;
                    case 6:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangHoiPhuc;
                        else
                            LuuKyNang[dem - 4] = KyNangHoiPhuc;
                        break;
                    case 7:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangChucPhucCuaThienThan;
                        else
                            LuuKyNang[dem - 4] = KyNangChucPhucCuaThienThan;
                        break;
                    case 8:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTanCongChiMang;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCongChiMang;
                        break;
                    case 9:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangPhaGiap;
                        else
                            LuuKyNang[dem - 4] = KyNangPhaGiap;
                        break;
                    case 10:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangSieuTangCuong;
                        else
                            LuuKyNang[dem - 4] = KyNangSieuTangCuong;
                        break;
                }
            }

            
            List<int> numbersAI = NgauNhien(1, so_luong_ky_nang+1);

            int demAI = 0;
            foreach (int numberAI in numbersAI)
            {
                demAI++;
                switch (numberAI)
                {
                    case 1:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCong;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCong;
                        break;
                    case 2:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangHoiMau;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangHoiMau;
                        break;
                    case 3:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangPhongThu;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangPhongThu;
                        break;
                    case 4:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTangCuong;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTangCuong;
                        break;
                    case 5:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangGiamSucManh;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangGiamSucManh;
                        break;
                    case 6:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangHoiPhuc;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangHoiPhuc;
                        break;
                    case 7:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangChucPhucCuaThienThan;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangChucPhucCuaThienThan;
                        break;
                    case 8:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCongChiMang;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCongChiMang;
                        break;
                    case 9:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangPhaGiap;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangPhaGiap;
                        break;
                    case 10:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangSieuTangCuong;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangSieuTangCuong;
                        break;
                }
            }

            nguoi_mau_toi_da = Nguoi.Mau;
            may_mau_toi_da = AI.Mau;

            //Debug Code
            /*
            for(int test = 1; test <= 4; test++)
            {
                MangKyNang[test] = KyNangSieuTangCuong;
                MangKyNangAI[test] = KyNangSieuTangCuong;
            }
            */
            


            int luu_vi_tri = 0;
            int kiem_tra_thu_tu = 0;
            bool kiem_tra_hanh_dong = false;

            int kiem_tra_thu_tu_da_dung = 0;
            int kiem_tra_thu_tu_da_dungAI = 0;

            int luu_vi_triAI = 0;
            int kiem_tra_thu_tuAI = 0;
            bool kiem_tra_hanh_dongAI = false;

            while (true)
            {
                if (kiem_tra_hanh_dong == true)
                {
                    kiem_tra_thu_tu_da_dung++;
                    DaDungKyNang[kiem_tra_thu_tu_da_dung] = MangKyNang[luu_vi_tri];

                    kiem_tra_thu_tu++;
                    kiem_tra_hanh_dong = false;

                    if (kiem_tra_thu_tu <= so_luong_ky_nang - 4)        
                        MangKyNang[luu_vi_tri] = LuuKyNang[kiem_tra_thu_tu];
                    else
                    {
                        MangKyNang[luu_vi_tri] = DaDungKyNang[1];
                        
                        for (int i = 1; i <= so_luong_ky_nang - 4; i++)
                            DaDungKyNang[i] = DaDungKyNang[i + 1];

                        kiem_tra_thu_tu_da_dung--;
                        kiem_tra_thu_tu--;
                    }    

                }

                
                if (kiem_tra_hanh_dongAI == true)
                {
                    kiem_tra_thu_tu_da_dungAI++;
                    DaDungKyNangAI[kiem_tra_thu_tu_da_dungAI] = MangKyNangAI[luu_vi_triAI];

                    kiem_tra_thu_tuAI++;
                    kiem_tra_hanh_dongAI = false;

                    if (kiem_tra_thu_tuAI <= so_luong_ky_nang - 4)
                        MangKyNangAI[luu_vi_triAI] = LuuKyNangAI[kiem_tra_thu_tuAI];
                    else
                    {
                        MangKyNangAI[luu_vi_triAI] = DaDungKyNangAI[1];

                        for (int i = 1; i <= so_luong_ky_nang - 4; i++)
                            DaDungKyNangAI[i] = DaDungKyNangAI[i + 1];

                        kiem_tra_thu_tu_da_dungAI--;
                        kiem_tra_thu_tuAI--;
                    }    
                }
                
                if (Nguoi.Mau <= 0)
                    GameOver();
                if (AI.Mau <= 0)
                    ChienThang();

                if (thoi_gian_may == false)
                    luot_di = true;
                if (thoi_gian_nguoi_choi == false)
                    luot_di = false;


                if (luot_di == true)
                    van_dau = "Lượt của bạn!";
                else
                    van_dau = "Lượt của máy!";
                    
                Console.SetCursorPosition(25, 5);
                Console.WriteLine("Thẻ: "+ AI.Ten +" Máu: "+ AI.Mau +"/"+ may_mau_toi_da+" Công: "+ AI.Cong + " Thủ: "+ AI.Thu);

                Console.SetCursorPosition(25, 10);
                Console.WriteLine(van_dau);
                
                Console.SetCursorPosition(25, 15);
                Console.WriteLine("Thẻ: " + Nguoi.Ten +" Máu: "+ Nguoi.Mau + "/" + nguoi_mau_toi_da + " Công: " + Nguoi.Cong + " Thủ: "+ Nguoi.Thu);
                Console.SetCursorPosition(27, 17);
                Console.WriteLine(MangKyNang[1].Ten);
                Console.SetCursorPosition(27, 19);
                Console.WriteLine(MangKyNang[2].Ten);
                Console.SetCursorPosition(27, 21);
                Console.WriteLine(MangKyNang[3].Ten);
                Console.SetCursorPosition(27, 23);
                Console.WriteLine(MangKyNang[4].Ten);
                Console.SetCursorPosition(27, 25);
                if(kiem_tra_ky_nang == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(Nguoi.Ky_nang_toi_thuong);
                    Console.ResetColor();
                }    
               

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
                            int vi_tri_y = 21;
                            if (kiem_tra_ky_nang == true)
                                vi_tri_y = 23;
                            if (y <= vi_tri_y)
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
                            if (kiem_tra_ky_nang == true)
                            {
                                if (y == 25)
                                    ThongTin(5);
                            }    
                            
                            Console.Clear();
                            break;

                        case ConsoleKey.K:
                            if (y == 17)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng "+ MangKyNang[1].Ten+ "!";
                                KyNang(MangKyNang[1].Id, luot_di);
                                luu_vi_tri = 1;
                                kiem_tra_hanh_dong = true;
                            }

                            if (y == 19)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng "+ MangKyNang[2].Ten + "!";
                                KyNang(MangKyNang[2].Id, luot_di);
                                luu_vi_tri = 2;
                                kiem_tra_hanh_dong = true;
                            }

                            if (y == 21)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng "+ MangKyNang[3].Ten + "!";
                                KyNang(MangKyNang[3].Id, luot_di);
                                luu_vi_tri = 3;
                                kiem_tra_hanh_dong = true;
                            }

                            if (y == 23)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng "+ MangKyNang[4].Ten + "!";
                                KyNang(MangKyNang[4].Id, luot_di);
                                luu_vi_tri = 4;
                                kiem_tra_hanh_dong = true;
                            }
                            if (y == 25)
                            {
                                van_dau = "Bạn vừa sử dụng thẻ kỹ năng tối thượng " + Nguoi.Ky_nang_toi_thuong + "!";
                                kiem_tra_ky_nang = false;
                                y = 17;
                                KyNangToiThuong(Nguoi.Id, luot_di);
                            }
                            luot_di = !luot_di;
                            Console.SetCursorPosition(25, 10);
                            for (int i = 0; i < van_dau_tam.Length; i++)
                                Console.WriteLine(" ");
                            Console.SetCursorPosition(25, 10);
                            Console.WriteLine(van_dau);                            
                            Thread.Sleep(1000);
                            HieuUng(luot_di);
                            Console.Clear();
                            break;

                        case ConsoleKey.Q:
                            return;
                    }
                }
                else
                {
                    Thread.Sleep(1000);
                    int id = AILuaChon();
                    if (id == 1)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng " + MangKyNangAI[1].Ten + "!";
                        KyNang(MangKyNangAI[1].Id, luot_di);
                        luu_vi_triAI = 1;
                        kiem_tra_hanh_dongAI = true;
                    }

                    if (id == 2)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng " + MangKyNangAI[2].Ten + "!";
                        KyNang(MangKyNangAI[2].Id, luot_di);
                        luu_vi_triAI = 2;
                        kiem_tra_hanh_dongAI = true;
                    }

                    if (id == 3)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng " + MangKyNangAI[3].Ten + "!";
                        KyNang(MangKyNangAI[3].Id, luot_di);
                        luu_vi_triAI = 3;
                        kiem_tra_hanh_dongAI = true;
                    }

                    if (id == 4)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng " + MangKyNangAI[4].Ten + "!";
                        KyNang(MangKyNangAI[4].Id, luot_di);
                        luu_vi_triAI = 4;
                        kiem_tra_hanh_dongAI = true;
                    }
                    if (id == 5)
                    {
                        van_dau = "Máy vừa sử dụng thẻ kỹ năng tối thượng " + AI.Ky_nang_toi_thuong + "!";
                        kiem_tra_ky_nangAI = false;
                        KyNangToiThuong(AI.Id, luot_di);
                    }
                    luot_di = !luot_di;
                    Console.SetCursorPosition(25, 10);
                    for (int i = 0; i < van_dau_tam.Length; i++)
                        Console.WriteLine(" ");
                    Console.SetCursorPosition(25, 10);
                    Console.WriteLine(van_dau);                  
                    Thread.Sleep(1000);
                    HieuUng(luot_di);
                    Console.Clear();
                }
                    
                Console.SetCursorPosition(x_tam, y_tam);
                Console.Write("  ");

            }

        }

        static int AILuaChon()
        {
            if (kiem_tra_ky_nangAI == true)
                return rd.Next(1, 6);
            else
                return rd.Next(1, 5);
        }

        static void ThongTin(int id)
        {
            while(true)
            {
                Console.SetCursorPosition(5, 5);
                switch (id)
                {
                    case 1:
                        Console.WriteLine(MangKyNang[1].Mo_ta);
                        break;
                    case 2:
                        Console.WriteLine(MangKyNang[2].Mo_ta);
                        break;
                    case 3:
                        Console.WriteLine(MangKyNang[3].Mo_ta);
                        break;
                    case 4:
                        Console.WriteLine(MangKyNang[4].Mo_ta);
                        break;
                    case 5:
                        Console.WriteLine(Nguoi.Mo_ta_ky_nang_toi_thuong);
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
                        AI.Mau = TanCong(Nguoi.Cong, AI.Thu, AI.Mau);
                    else
                        Nguoi.Mau = TanCong(AI.Cong, Nguoi.Thu, Nguoi.Mau);
                    break;
                case 2:
                    if (luot_di == true)
                        Nguoi.Mau = HoiMau(Nguoi.Mau, nguoi_mau_toi_da);
                    else
                        AI.Mau = HoiMau(AI.Mau, may_mau_toi_da);
                    break;
                case 3:
                    if (luot_di == true)
                        Nguoi.Thu = PhongThu(Nguoi.Thu);
                    else
                        AI.Thu = PhongThu(AI.Thu);
                    break;
                case 4:
                    if (luot_di == true)
                        Nguoi.Cong = TangCuong(Nguoi.Cong);
                    else
                        AI.Cong = TangCuong(AI.Cong);
                    break;
                case 5:
                    if (luot_di == true)
                    {
                        AI.Cong = GiamSucManh(AI.Cong);
                        giam_cong_may = true;
                    }    
                    else
                    {
                        Nguoi.Cong = GiamSucManh(Nguoi.Cong);
                        giam_cong_nguoi_choi = true;
                    }    
                    break;
                case 6:
                    if (luot_di == true)
                        Nguoi.Mau = HoiPhuc(Nguoi.Mau, nguoi_mau_toi_da);
                    else
                        AI.Mau = HoiPhuc(AI.Mau, may_mau_toi_da);
                    break;
                case 7:
                    if (luot_di == true)
                        nguoi_mau_toi_da = ChucPhucCuaThienThan(nguoi_mau_toi_da);
                    else
                        may_mau_toi_da = ChucPhucCuaThienThan(may_mau_toi_da);
                    break;
                case 8:
                    if (luot_di == true)
                        AI.Mau = TanCongChiMang(Nguoi.Cong, AI.Thu, AI.Mau);
                    else
                        Nguoi.Mau = TanCongChiMang(AI.Cong, Nguoi.Thu, Nguoi.Mau);
                    break;
                case 9:
                    if (luot_di == true)
                    {
                        luu_thu_may = AI.Thu;
                        AI.Thu = PhaGiap();
                        giam_thu_may = true;
                        luu_dem_pha_giap_may = 2;
                    }    
                    else
                    {
                        luu_thu_nguoi_choi = Nguoi.Thu;
                        Nguoi.Thu = PhaGiap();
                        giam_thu_nguoi_choi = true;
                        luu_dem_pha_giap_nguoi_choi = 2;
                    }    
                    break;
                case 10:
                    if (luot_di == true)
                    {
                        luu_cong_nguoi_choi = Nguoi.Cong;
                        Nguoi.Cong = SieuTangCuong(Nguoi.Cong);
                        tang_cong_nguoi_choi = true;
                        luu_dem_sieu_tang_cuong_nguoi_choi = 2;
                    }                           
                    else
                    {
                        luu_cong_may = AI.Cong;
                        AI.Cong = SieuTangCuong(AI.Cong);
                        tang_cong_may = true;
                        luu_dem_sieu_tang_cuong_may = 2;
                    }    
                       
                    break;
            }
        }

        static void KyNangToiThuong(int id, bool luot_di)
        {
            switch (id)
            {
                case 1:
                    if (luot_di == true)
                        AI.Mau = PhaoHoaNguc(Nguoi.Cong, AI.Thu, AI.Mau);
                    else
                        Nguoi.Mau = PhaoHoaNguc(AI.Cong, Nguoi.Thu, Nguoi.Mau);
                    break;
                case 2:
                    if (luot_di == true)
                        DieuChinhThoiGian(1);
                    else
                        DieuChinhThoiGian(2);
                    break;
            }
        }

        static void HieuUng(bool kiem_tra)
        {
            if (kiem_tra == true) // Người
            {
                if (giam_cong_may == true) // 1 lượt thì bình thường
                {
                    giam_cong_may = false;
                    AI.Cong *= 2;
                }

                if (thoi_gian_nguoi_choi == false)
                {
                    if (dem_thoi_gian == 0)
                        thoi_gian_nguoi_choi = true;
                    dem_thoi_gian--;
                }

                if (giam_thu_nguoi_choi == true) // 2 lượt thì đảo lại hiệu ứng của máy để bên người và hiệu ứng người để bên máy
                {
                    if (luu_dem_pha_giap_nguoi_choi == 1)
                    {
                        giam_thu_nguoi_choi = false;
                        Nguoi.Thu = luu_thu_nguoi_choi;
                    }
                    else
                        luu_dem_pha_giap_nguoi_choi--;
                }

                if (tang_cong_may == true) // 2 lượt
                {
                    if (luu_dem_sieu_tang_cuong_may == 1)
                    {
                        tang_cong_may = false;
                        AI.Cong = luu_cong_may;
                    }
                    else
                        luu_dem_sieu_tang_cuong_may--;
                }


            }
            else // Máy
            {
                if (giam_cong_nguoi_choi == true) // 1 lượt
                {
                    giam_cong_nguoi_choi = false;
                    Nguoi.Cong *= 2;
                }

                if (thoi_gian_may == false)
                {
                    if (dem_thoi_gian == 0)
                        thoi_gian_may = true;
                    dem_thoi_gian--;
                }

                if (giam_thu_may == true) // 2 lượt
                {
                    if (luu_dem_pha_giap_may == 1)
                    {
                        giam_thu_may = false;
                        AI.Thu = luu_thu_may;
                    }
                    else
                        luu_dem_pha_giap_may--;
                }

                if (tang_cong_nguoi_choi == true) // 2 lượt
                {
                    if (luu_dem_sieu_tang_cuong_nguoi_choi == 1)
                    {
                        tang_cong_nguoi_choi = false;
                        Nguoi.Cong = luu_cong_nguoi_choi;
                    }
                    else
                        luu_dem_sieu_tang_cuong_nguoi_choi--;
                }
 
            }
            
        }

        static void GameOver()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("GAME OVER");
            Thread.Sleep(3000);
            Reset();
            Console.Clear();
            menu();
        }

        static void ChienThang()
        {
            Console.Clear();
            Console.SetCursorPosition(5, 5);
            Console.WriteLine("Chúc mừng bạn đã chiến thắng!");
            Thread.Sleep(3000);
            Reset();
            Console.Clear();
            menu();
        }

    }
}
