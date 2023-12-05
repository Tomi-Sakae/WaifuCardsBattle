using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace WaifuCardsBattle
{
    // Waifu Cards Battle Code viết bởi Tomi Sakae (https://github.com/Tomi-Sakae/WaifuCardsBattle).
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

        static public int PhaoHoaNguc(int cong, int thu)
        {
           
            if (((cong + ((cong * 500) / 100)) - thu) <= 0)
                return 0;
            else
                return cong + ((cong * 500) / 100);
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
        static public int TanCong(int cong, int thu)
        {
            if ((cong - thu) <= 0)
                return 0;
            else
                return cong - thu;
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

        static TheKyNang KyNangGiamSucManh = new TheKyNang(5, "Giảm Sức Mạnh", "Thẻ Giảm Sức Mạnh: Giảm một nửa sức tấn công của đối thủ trong lượt kế.\n\n\tKhi kết thúc lượt nhân đôi sức tấn công hiện tại của đối thủ.");
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
        static public int TanCongChiMang(int cong, int thu)
        {
            if ((cong*2 - thu) <= 0)
                return 0;
            else
                return cong*2 - thu;
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

        static TheKyNang KyNangGiapAo = new TheKyNang(11, "Giáp Ảo", "Thẻ Giáp Ảo: X2 chỉ số thủ hiện tại ở lượt kế.");
        static public int GiapAo(int thu)
        {
            return thu * 2;
        }

        static bool tang_giap_nguoi_choi = false;
        static bool tang_giap_may = false;
        static int luu_giap_nguoi_choi;
        static int luu_giap_may;
        static int luu_dem_giap_ao_nguoi_choi;
        static int luu_dem_giap_ao_may;

        static TheKyNang KyNangMauAo = new TheKyNang(12, "Máu Ảo", "Thẻ Máu Ảo: Tăng máu lên tối đa ở lượt kế.");
        static public int MauAo(int mau_toi_da)
        {
            return  mau_toi_da;
        }

        static bool tang_mau_ao_nguoi_choi = false;
        static bool tang_mau_ao_may = false;
        static int luu_mau_ao_nguoi_choi;
        static int luu_mau_ao_may;
        static int luu_dem_mau_ao_nguoi_choi;
        static int luu_dem_mau_ao_may;

        static TheKyNang KyNangTanCongTrongKich = new TheKyNang(13, "Tấn Công Trọng Kích", "Thẻ Tấn Công Trọng Kích: Tấn công đối thủ với 50% sức tấn công, đồng thời giảm 1 thủ của đối phương.");
        static public int TanCongTrongKich(int cong, int thu)
        {
            if ((((cong * 50)/100) - thu) <= 0)
                return 0;
            else
                return ((cong * 50) / 100) - thu;
        }

        static TheKyNang KyNangSatThuongCuoiCung = new TheKyNang(14, "Sát Thương Cuối Cùng", "Thẻ Sát Thương Cuối Cùng: Tấn công đối thủ với sức tấn công hiện tại + 10% lượng máu hiện tại/máu tối đa.\n\n\tMáu hiện tại càng ít thì sát thương càng lớn!");
        static public int SatThuongCuoiCung(int cong, int thu, int mau_nguoi_choi, int mau_toi_da)
        {
            if ((cong + (((mau_toi_da-mau_nguoi_choi) * 10) / 100) - thu) <= 0)
                return 0;
            else
                return cong + (((mau_toi_da - mau_nguoi_choi) * 10) / 100) - thu;
        }

        static TheKyNang KyNangTanCongPhucHoi = new TheKyNang(15, "Tấn Công Phục Hồi", "Thẻ Tấn Công Phục Hồi: Hồi máu với lượt sát thương gây ra khi tấn công đối thủ.");
        static public int TanCongPhucHoi(int cong, int thu)
        {
            if ((cong - thu) <= 0)
                return 0;
            else
                return cong - thu;
        }

        static TheKyNang KyNangKhienBaoVe = new TheKyNang(16, "Khiên Bảo Vệ", "Thẻ Khiên Bảo Vệ: Kháng toàn bộ sát thương của đối thủ gây ra ở lượt kế.");
        static public void KhienBaoVe()
        {
            
        }

        static bool khien_bao_ve_nguoi_choi = false;
        static bool khien_bao_ve_may = false;
        static int luu_dem_khien_bao_ve_nguoi_choi;
        static int luu_dem_khien_bao_ve_may;

        static TheKyNang KyNangDaoNguoc = new TheKyNang(17, "Đảo Ngược", "Thẻ Đảo Ngược: Đảo ngược tác dụng của mọi kỹ năng mà đối thủ sử dụng ở lượt kế.");
        static public void DaoNguoc()
        {

        }

        static bool dao_nguoc_nguoi_choi = false;
        static bool dao_nguoc_may = false;

        static TheKyNang KyNangHienThucHoa = new TheKyNang(18, "Hiện Thực Hóa", "Thẻ Hiện Thực Hóa: Hiện thực hóa tất cả các trạng thái ảo hiện tại của cả ván đấu.");
        static public void HienThucHoa()
        {

        }

        static bool hien_thuc_hoa = false;

        static TheKyNang KyNangSongAnhChiSo = new TheKyNang(19, "Song Ánh Chỉ Số", "Thẻ Song Ánh Chỉ Số: Phản ánh tất cả chỉ số hiện tại của đối thủ lên bản thân.");
        static public void SongAnhChiSo()
        {

        }

        static TheKyNang KyNangBomHenGio = new TheKyNang(20, "Bom Hẹn Giờ", "Thẻ Bom Hẹn Giờ: Đặt 1 quả bom hẹn giờ lên đối thủ với số lượt tùy chỉnh.\n\n\tSau khi phát nổ sẽ gây ra 50% sát thương so với sức tấn công tại thời điểm phát nổ.\n\n\tBỏ qua chỉ số thủ!");
        static public void BomHenGio()
        {

        }

        static int luot_cho_bom_hen_gio_nguoi_choi;
        static int luot_cho_bom_hen_gio_may;
        static bool kiem_tra_bom_hen_gio_nguoi_choi = false;
        static bool kiem_tra_bom_hen_gio_may = false;

        static TheKyNang KyNangCuongHoa = new TheKyNang(21, "Cường Hóa", "Thẻ Cường Hóa: Cường hóa sát thương ở lượt kế lên gấp đôi.");
        static public void CuongHoa()
        {

        }

        static bool cuong_hoa_nguoi_choi = false;
        static bool cuong_hoa_may = false;
        static int luu_dem_cuong_hoa_nguoi_choi;
        static int luu_dem_cuong_hoa_may;

        static TheKyNang KyNangSuyYeu = new TheKyNang(22, "Suy Yếu", "Thẻ Suy Yếu: Làm lượng sát thương của đối thủ giảm một nửa ở lượt kế.");
        static public void SuyYeu()
        {

        }

        static bool suy_yeu_nguoi_choi = false;
        static bool suy_yeu_may = false;
        static int luu_dem_suy_yeu_nguoi_choi;
        static int luu_dem_suy_yeu_may;

        static TheKyNang KyNangAoAnh = new TheKyNang(23, "Ảo Ảnh", "Thẻ Ảo Ảnh: Làm cho tên của tất cả các kỹ năng của đối thủ bị biến thành tên mình ở lượt kế.");
        static public void AoAnh()
        {

        }

        static bool ao_anh_nguoi_choi = false;
        static bool ao_anh_may = false;

        static TheKyNang KyNangHoanDoiChiSo = new TheKyNang(24, "Hoán Đổi Chỉ Số", "Thẻ Hoán Đổi Chỉ Số: Làm cho tất cả chỉ số của bản thân bị hoán đổi ngẫu nhiên.");
        static public void HoanDoiChiSo()
        {

        }

        static TheKyNang KyNangTanCongXuyenGiap = new TheKyNang(25, "Tấn Công Xuyên Giáp", "Thẻ Tấn Công Xuyên Giáp: Tấn công đối thủ với toàn bộ sát thương dựa trên sức tấn công hiện tại và bỏ qua chỉ số thủ.");
        static public int TanCongXuyenGiap(int cong)
        {
            return cong;
        }

        static TheKyNang KyNangChuyenDoiSatThuong = new TheKyNang(26, "Chuyển Đổi Sát Thương", "Thẻ Chuyển Đổi Sát Thương: Chuyển lượng sát thương nhận được thành lượng máu hồi phục trong lượt kế.");
        static public void ChuyenDoiSatThuong()
        {
            
        }

        static bool chuyen_doi_sat_thuong_nguoi_choi = false;
        static bool chuyen_doi_sat_thuong_may = false;
        static int luu_dem_chuyen_doi_sat_thuong_nguoi_choi;
        static int luu_dem_chuyen_doi_sat_thuong_may;

        static TheKyNang KyNangDoiCho = new TheKyNang(27, "Đổi Chổ", "Thẻ Đổi Chổ: Chuyển lượng máu tối đa thành lượng máu hiện tại và ngược lại.");
        static public void DoiCho()
        {

        }

        static TheKyNang KyNangGiapGai = new TheKyNang(28, "Giáp Gai", "Thẻ Giáp Gai: Đối thủ khi gây sát thương ở lượt kế sẽ bị phản ngược lại.");
        static public void GiapGai()
        {

        }

        static bool giap_gai_nguoi_choi = false;
        static bool giap_gai_may = false;
        static int luu_dem_giap_gai_nguoi_choi;
        static int luu_dem_giap_gai_may;

        static TheKyNang KyNangQuayRoi = new TheKyNang(29, "Quấy Rối", "Thẻ Quấy Rối: Làm cho đối thủ ở lượt sau không thể tự lựa chọn kỹ năng.");
        static public void QuayRoi()
        {

        }

        static bool quay_roi_nguoi_choi = false;
        static bool quay_roi_may = false;

        static TheKyNang KyNangEpBuoc = new TheKyNang(30, "Ép Buộc", "Thẻ Ép Buộc: Người dùng kỹ năng này sẽ buộc đối thủ phải dùng kỹ năng mà mình chọn ở lượt kế.");
        static public void EpBuoc()
        {

        }

        static bool ep_buoc_nguoi_choi = false;
        static bool ep_buoc_may = false;

        static TheKyNang KyNangLoiKeoCuaTuThan = new TheKyNang(31, "Lôi Kéo Của Tử Thần", "Thẻ Lôi Kéo Của Tử Thần: Giảm 10% máu tối đa của đối thủ.");
        static public int LoiKeoCuaTuThan(int mau_toi_da)
        {
            return mau_toi_da - (mau_toi_da * 10) / 100;
        }

        static TheKyNang KyNangXienGiap = new TheKyNang(32, "Xiên Giáp", "Thẻ Xiên Giáp: Giảm 1 thủ của đối thủ.");
        static public int XienGiap(int thu)
        {
            return --thu;
        }

        static TheKyNang KyNangSuyGiam = new TheKyNang(33, "Suy Giảm", "Thẻ Suy Giảm: Giảm 1 công của đối thủ.");
        static public int SuyGiam(int cong)
        {
            return --cong;
        }

        static TheKyNang KyNangChuyenDoiKyNang = new TheKyNang(34, "Chuyển Đổi Kỹ Năng", "Thẻ Chuyển Đổi Kỹ Năng: Chọn ra một kỹ năng hiện có và trao đổi với kỹ năng ngẫu nhiên của đối thủ.");
        static public void ChuyenDoiKyNang()
        {
            
        }

        static TheKyNang KyNangLuaChonMaQuai = new TheKyNang(35, "Lựa Chọn Ma Quái", "Thẻ Lựa Chọn Ma Quái: Chọn ra và lấy một kỹ năng nằm trong danh sách những kỹ năng đã sử dụng hoặc chưa sử dụng.");
        static public void LuaChonMaQuai()
        {

        }

        static TheKyNang KyNangTanCongQuyDoi = new TheKyNang(36, "Tấn Công Quy Đổi", "Thẻ Tấn Công Quy Đổi: Tấn công đối thủ với lượng sát thương = lượng máu quy đổi.");
        static public void TanCongQuyDoi()
        {

        }

        static TheKyNang KyNangTanCongXuyenPha = new TheKyNang(37, "Tấn Công Xuyên Phá", "Thẻ Tấn Công Xuyên Phá: Tấn công đối thủ với 200% sức tấn công hiện tại và bản thân cũng bị ảnh hưởng.");
        static public int TanCongXuyenPha(int cong, int thu)
        {
            if ((cong * 2 - thu) <= 0)
                return 0;
            else
                return cong * 2 - thu;
        }

        static TheKyNang[] MangKyNang = new TheKyNang[100];
        static TheKyNang[] MangKyNangAI = new TheKyNang[100];
        static int so_luong_ky_nang = 37;

        static int nguoi_mau_toi_da;
        static int may_mau_toi_da;
        static bool kiem_tra_ky_nangAI = true;
        static int luu_id_nguoi;

        static TheKyNang[] LuuKyNang = new TheKyNang[100];
        static TheKyNang[] LuuKyNangAI = new TheKyNang[100];

        static TheKyNang[] DaDungKyNang = new TheKyNang[100];
        static TheKyNang[] DaDungKyNangAI = new TheKyNang[100];

        static int sat_thuong_nguoi_choi;
        static int sat_thuong_may;
        static bool luot_di_tam = false;
        static bool choi_co_op = false;

        static int luu_vi_tri = 0;
        static int kiem_tra_thu_tu = 0;
        static bool kiem_tra_hanh_dong = false;

        static int kiem_tra_thu_tu_da_dung = 0;
        static int kiem_tra_thu_tu_da_dungAI = 0;

        static int luu_vi_triAI = 0;
        static int kiem_tra_thu_tuAI = 0;
        static bool kiem_tra_hanh_dongAI = false;

        static int x_giao_dien_nguoi_choi_tam;
        static int y_giao_dien_nguoi_choi_tam;
        static string van_dau_giao_dien_nguoi_choi_tam;
        static bool kiem_tra_ky_nang_giao_dien_nguoi_choi_tam;

        static int x_giao_dien_may_tam;
        static int y_giao_dien_may_tam;
        static string van_dau_giao_dien_may_tam;
        static bool kiem_tra_ky_nang_giao_dien_may_tam;

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
                Console.WriteLine("Co-op");
                Console.SetCursorPosition(7, 11);
                Console.WriteLine("Giới Thiệu");
                Console.SetCursorPosition(7, 13);
                Console.WriteLine("Thoát");


                Console.SetCursorPosition(x, y);
                Console.Write("->");


                x_tam = x;
                y_tam = y;

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (y >= 9)
                            y -= 2;
                        break;

                    case ConsoleKey.DownArrow:
                        if (y <= 11)
                            y += 2;
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (y == 7)
                            Choi();
                        if (y == 9)
                            Coop();
                        if (y == 11)
                            GioiThieu();
                        if (y == 13)
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
                    case ConsoleKey.Backspace:
                        return;
                }
            }

        }

        static void Thoat()
        {
            Environment.Exit(0);
        }

        static void Coop()
        {
            choi_co_op = true;
            Choi();
        }

        static void Choi()
        {
            string van_dau = "";
            bool luot_di = true;
            int x = 25, y = 20;
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
                    case 11:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangGiapAo;
                        else
                            LuuKyNang[dem - 4] = KyNangGiapAo;
                        break;
                    case 12:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangMauAo;
                        else
                            LuuKyNang[dem - 4] = KyNangMauAo;
                        break;
                    case 13:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTanCongTrongKich;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCongTrongKich;
                        break;
                    case 14:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangSatThuongCuoiCung;
                        else
                            LuuKyNang[dem - 4] = KyNangSatThuongCuoiCung;
                        break;
                    case 15:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTanCongPhucHoi;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCongPhucHoi;
                        break;
                    case 16:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangKhienBaoVe;
                        else
                            LuuKyNang[dem - 4] = KyNangKhienBaoVe;
                        break;
                    case 17:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangDaoNguoc;
                        else
                            LuuKyNang[dem - 4] = KyNangDaoNguoc;
                        break;
                    case 18:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangHienThucHoa;
                        else
                            LuuKyNang[dem - 4] = KyNangHienThucHoa;
                        break;
                    case 19:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangSongAnhChiSo;
                        else
                            LuuKyNang[dem - 4] = KyNangSongAnhChiSo;
                        break;
                    case 20:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangBomHenGio;
                        else
                            LuuKyNang[dem - 4] = KyNangBomHenGio;
                        break;
                    case 21:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangCuongHoa;
                        else
                            LuuKyNang[dem - 4] = KyNangCuongHoa;
                        break;
                    case 22:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangSuyYeu;
                        else
                            LuuKyNang[dem - 4] = KyNangSuyYeu;
                        break;
                    case 23:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangAoAnh;
                        else
                            LuuKyNang[dem - 4] = KyNangAoAnh;
                        break;
                    case 24:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangHoanDoiChiSo;
                        else
                            LuuKyNang[dem - 4] = KyNangHoanDoiChiSo;
                        break;
                    case 25:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTanCongXuyenGiap;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCongXuyenGiap;
                        break;
                    case 26:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangChuyenDoiSatThuong;
                        else
                            LuuKyNang[dem - 4] = KyNangChuyenDoiSatThuong;
                        break;
                    case 27:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangDoiCho;
                        else
                            LuuKyNang[dem - 4] = KyNangDoiCho;
                        break;
                    case 28:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangGiapGai;
                        else
                            LuuKyNang[dem - 4] = KyNangGiapGai;
                        break;
                    case 29:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangQuayRoi;
                        else
                            LuuKyNang[dem - 4] = KyNangQuayRoi;
                        break;
                    case 30:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangEpBuoc;
                        else
                            LuuKyNang[dem - 4] = KyNangEpBuoc;
                        break;
                    case 31:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangLoiKeoCuaTuThan;
                        else
                            LuuKyNang[dem - 4] = KyNangLoiKeoCuaTuThan;
                        break;
                    case 32:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangXienGiap;
                        else
                            LuuKyNang[dem - 4] = KyNangXienGiap;
                        break;
                    case 33:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangSuyGiam;
                        else
                            LuuKyNang[dem - 4] = KyNangSuyGiam;
                        break;
                    case 34:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangChuyenDoiKyNang;
                        else
                            LuuKyNang[dem - 4] = KyNangChuyenDoiKyNang;
                        break;
                    case 35:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangLuaChonMaQuai;
                        else
                            LuuKyNang[dem - 4] = KyNangLuaChonMaQuai;
                        break;
                    case 36:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTanCongQuyDoi;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCongQuyDoi;
                        break;
                    case 37:
                        if (dem <= 4)
                            MangKyNang[dem] = KyNangTanCongXuyenPha;
                        else
                            LuuKyNang[dem - 4] = KyNangTanCongXuyenPha;
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
                    case 11:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangGiapAo;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangGiapAo;
                        break;
                    case 12:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangMauAo;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangMauAo;
                        break;
                    case 13:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCongTrongKich;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCongTrongKich;
                        break;
                    case 14:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangSatThuongCuoiCung;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangSatThuongCuoiCung;
                        break;
                    case 15:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCongPhucHoi;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCongPhucHoi;
                        break;
                    case 16:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangKhienBaoVe;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangKhienBaoVe;
                        break;
                    case 17:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangDaoNguoc;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangDaoNguoc;
                        break;
                    case 18:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangHienThucHoa;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangHienThucHoa;
                        break;
                    case 19:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangSongAnhChiSo;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangSongAnhChiSo;
                        break;
                    case 20:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangBomHenGio;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangBomHenGio;
                        break;
                    case 21:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangCuongHoa;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangCuongHoa;
                        break;
                    case 22:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangSuyYeu;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangSuyYeu;
                        break;
                    case 23:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangAoAnh;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangAoAnh;
                        break;
                    case 24:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangHoanDoiChiSo;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangHoanDoiChiSo;
                        break;
                    case 25:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCongXuyenGiap;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCongXuyenGiap;
                        break;
                    case 26:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangChuyenDoiSatThuong;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangChuyenDoiSatThuong;
                        break;
                    case 27:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangDoiCho;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangDoiCho;
                        break;
                    case 28:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangGiapGai;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangGiapGai;
                        break;
                    case 29:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangQuayRoi;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangQuayRoi;
                        break;
                    case 30:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangEpBuoc;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangEpBuoc;
                        break;
                    case 31:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangLoiKeoCuaTuThan;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangLoiKeoCuaTuThan;
                        break;
                    case 32:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangXienGiap;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangXienGiap;
                        break;
                    case 33:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangSuyGiam;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangSuyGiam;
                        break;
                    case 34:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangChuyenDoiKyNang;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangChuyenDoiKyNang;
                        break;
                    case 35:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangLuaChonMaQuai;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangLuaChonMaQuai;
                        break;
                    case 36:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCongQuyDoi;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCongQuyDoi;
                        break;
                    case 37:
                        if (demAI <= 4)
                            MangKyNangAI[demAI] = KyNangTanCongXuyenPha;
                        else
                            LuuKyNangAI[demAI - 4] = KyNangTanCongXuyenPha;
                        break;
                }
            }

            nguoi_mau_toi_da = Nguoi.Mau;
            may_mau_toi_da = AI.Mau;

            //Debug Code
            /*
            for(int test = 1; test <= 4; test++)
            {
                MangKyNang[test] = KyNangDaoNguoc;
                MangKyNangAI[test] = KyNangDaoNguoc;
            }
            */
            MangKyNang[1] = KyNangXienGiap;
            MangKyNangAI[1] = KyNangXienGiap;
            MangKyNang[2] = KyNangTanCongXuyenPha;
            MangKyNangAI[2] = KyNangTanCongXuyenPha;
            MangKyNang[3] = KyNangTanCongQuyDoi;
            MangKyNangAI[3] = KyNangTanCongQuyDoi;
            MangKyNang[4] = KyNangLoiKeoCuaTuThan;
            MangKyNangAI[4] = KyNangLoiKeoCuaTuThan;
            
            // Debug Code

            while (true)
            {
                sat_thuong_nguoi_choi = 0;
                sat_thuong_may = 0;
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
                
                if (thoi_gian_may == false) // Điều chỉnh thời gian
                    luot_di = true;
                if (thoi_gian_nguoi_choi == false) // Điều chỉnh thời gian
                    luot_di = false;

                if(choi_co_op == false)
                {
                    if (luot_di == true)
                        van_dau = "Lượt của bạn!";
                    else
                        van_dau = "Lượt của đối thủ!";
                }
                else
                {
                    if (luot_di == true)
                        van_dau = "Lượt của P1!";
                    else
                        van_dau = "Lượt của P2!";

                    if (ep_buoc_nguoi_choi == true)
                        van_dau = "Lượt của P1(P2?)!";
                    if (ep_buoc_may == true)
                        van_dau = "Lượt của P2(P1?)!";
                }
                
                if (choi_co_op == false)
                {
                    if (Nguoi.Mau <= 0)
                        van_dau = "Bạn đã thua:<";

                    if (AI.Mau <= 0)
                        van_dau = "Bạn đã chiến thắng!";
                }
                else
                {
                    if (Nguoi.Mau <= 0)
                        van_dau = "Người chơi 2 đã chiến thắng";

                    if (AI.Mau <= 0)
                        van_dau = "Người chơi 1 đã chiến thắng!";
                }

                GiaoDienNguoiChoi(x, y, van_dau, kiem_tra_ky_nang);
                x_tam = x;
                y_tam = y;

                string van_dau_tam = van_dau;
                if (Nguoi.Mau > 0 && AI.Mau > 0)
                {
                    if (luot_di == true)
                    {
                        ConsoleKeyInfo lua_chon;
                        if (choi_co_op == false)
                        {                           
                            if (quay_roi_nguoi_choi == false && ep_buoc_nguoi_choi == false) // Quấy rối
                                lua_chon = Console.ReadKey();
                            else if (quay_roi_nguoi_choi == true || ep_buoc_nguoi_choi == true) // Ép buộc
                            {
                                if (ep_buoc_nguoi_choi == true)
                                    Thread.Sleep(1000);
                                lua_chon = new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false);
                                int lua_chon_nguoi_choi;
                                if (kiem_tra_ky_nang == true)
                                    lua_chon_nguoi_choi = rd.Next(1, 6);
                                else
                                    lua_chon_nguoi_choi = rd.Next(1, 5);
                                if (lua_chon_nguoi_choi == 1)
                                    y = 20;
                                if (lua_chon_nguoi_choi == 2)
                                    y = 22;
                                if (lua_chon_nguoi_choi == 3)
                                    y = 24;
                                if (lua_chon_nguoi_choi == 4)
                                    y = 26;
                                if (lua_chon_nguoi_choi == 5)
                                    y = 28;
                            }
                            else
                                lua_chon = Console.ReadKey();
                        }                           
                        else
                        {
                            if (quay_roi_nguoi_choi == true)
                            {
                                lua_chon = new ConsoleKeyInfo('\r', ConsoleKey.Enter, false, false, false);
                                int lua_chon_nguoi_choi;
                                if (kiem_tra_ky_nang == true)
                                    lua_chon_nguoi_choi = rd.Next(1, 6);
                                else
                                    lua_chon_nguoi_choi = rd.Next(1, 5);
                                if (lua_chon_nguoi_choi == 1)
                                    y = 20;
                                if (lua_chon_nguoi_choi == 2)
                                    y = 22;
                                if (lua_chon_nguoi_choi == 3)
                                    y = 24;
                                if (lua_chon_nguoi_choi == 4)
                                    y = 26;
                                if (lua_chon_nguoi_choi == 5)
                                    y = 28;
                            }
                            else
                                lua_chon = Console.ReadKey();
                        }    
                            
                        luot_di_tam = luot_di;
                        if (dao_nguoc_nguoi_choi == true) // Đảo ngược
                            luot_di_tam = !luot_di_tam;

                        x_giao_dien_nguoi_choi_tam = x;
                        y_giao_dien_nguoi_choi_tam = y;
                        van_dau_giao_dien_nguoi_choi_tam = van_dau;
                        kiem_tra_ky_nang_giao_dien_nguoi_choi_tam = kiem_tra_ky_nang;

                        switch (lua_chon.Key)
                        {
                            case ConsoleKey.UpArrow:
                                if (y >= 22)
                                    y -= 2;
                                break;

                            case ConsoleKey.DownArrow:
                                int vi_tri_y = 24;
                                if (kiem_tra_ky_nang == true)
                                    vi_tri_y = 26;
                                if (y <= vi_tri_y)
                                    y += 2;
                                break;

                            case ConsoleKey.P:
                                Console.Clear();
                                if (y == 20)
                                    ThongTin(1);
                                if (y == 22)
                                    ThongTin(2);
                                if (y == 24)
                                    ThongTin(3);
                                if (y == 26)
                                    ThongTin(4);
                                if (kiem_tra_ky_nang == true)
                                {
                                    if (y == 28)
                                        ThongTin(5);
                                }

                                Console.Clear();
                                break;

                            case ConsoleKey.Enter:
                                if (y == 20)
                                {
                                    luu_vi_tri = 1;
                                    kiem_tra_hanh_dong = true;
                                    van_dau = Nguoi.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNang[1].Ten + "!";
                                    KyNang(MangKyNang[1].Id, luot_di_tam);                              
                                }

                                if (y == 22)
                                {
                                    luu_vi_tri = 2;
                                    kiem_tra_hanh_dong = true;
                                    van_dau = Nguoi.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNang[2].Ten + "!";
                                    KyNang(MangKyNang[2].Id, luot_di_tam);                                    
                                }

                                if (y == 24)
                                {
                                    luu_vi_tri = 3;
                                    kiem_tra_hanh_dong = true;
                                    van_dau = Nguoi.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNang[3].Ten + "!";
                                    KyNang(MangKyNang[3].Id, luot_di_tam);                                    
                                }

                                if (y == 26)
                                {
                                    luu_vi_tri = 4;
                                    kiem_tra_hanh_dong = true;
                                    van_dau = Nguoi.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNang[4].Ten + "!";
                                    KyNang(MangKyNang[4].Id, luot_di_tam);                                   
                                }
                                if (y == 28)
                                {
                                    van_dau = Nguoi.Ten + " vừa sử dụng thẻ kỹ năng tối thượng " + Nguoi.Ky_nang_toi_thuong + "!";
                                    kiem_tra_ky_nang = false;
                                    y = 20;
                                    KyNangToiThuong(Nguoi.Id, luot_di_tam);
                                }

                                if (suy_yeu_nguoi_choi == true) // Suy yếu
                                    sat_thuong_nguoi_choi /= 2;

                                if (cuong_hoa_nguoi_choi == true) // Cường hóa
                                    sat_thuong_nguoi_choi *= 2;

                                if (khien_bao_ve_may == true) // Khiên bảo vệ
                                    sat_thuong_nguoi_choi = 0;

                                if (chuyen_doi_sat_thuong_nguoi_choi == true) // Chuyển đổi sát thương
                                    AI.Mau += sat_thuong_nguoi_choi;

                                if (giap_gai_may == false) // Giáp gai
                                {
                                    if (luot_di_tam == true && chuyen_doi_sat_thuong_nguoi_choi == false) // Đảo ngược
                                        AI.Mau -= sat_thuong_nguoi_choi;
                                    else if (luot_di_tam == false && chuyen_doi_sat_thuong_may == false)
                                        Nguoi.Mau -= sat_thuong_may;
                                }
                                else
                                    Nguoi.Mau -= sat_thuong_nguoi_choi;

                                if (ao_anh_nguoi_choi == true) // Ảo ảnh              
                                    ao_anh_nguoi_choi = false;

                                if (quay_roi_nguoi_choi == true) // Quấy rối
                                    Thread.Sleep(1000);

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

                            case ConsoleKey.Backspace:
                                return;
                        }
                    }
                    else
                    {                       
                        int id = 0;
                        if (choi_co_op == false || quay_roi_may == true) // Quấy rối
                        {
                            Thread.Sleep(1000);
                            id = AILuaChon();
                        }                               
                        else if (choi_co_op == true || quay_roi_may == false)
                            id = NguoiLuaChon(van_dau, kiem_tra_ky_nang);

                        if (ep_buoc_may == true && choi_co_op == false)
                            id = NguoiLuaChon(van_dau, kiem_tra_ky_nang);
                        else if (ep_buoc_may == false && choi_co_op == false)
                            id = AILuaChon();

                        luot_di_tam = luot_di;
                        if (dao_nguoc_may == true) // Đảo ngược
                            luot_di_tam = !luot_di_tam;

                        x_giao_dien_may_tam = x;
                        y_giao_dien_may_tam = y;
                        van_dau_giao_dien_may_tam = van_dau;
                        kiem_tra_ky_nang_giao_dien_may_tam = kiem_tra_ky_nangAI;

                        if (id == 1)
                        {
                            van_dau = AI.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNangAI[1].Ten + "!";
                            KyNang(MangKyNangAI[1].Id, luot_di_tam);
                            luu_vi_triAI = 1;
                            kiem_tra_hanh_dongAI = true;
                        }

                        if (id == 2)
                        {
                            van_dau = AI.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNangAI[2].Ten + "!";
                            KyNang(MangKyNangAI[2].Id, luot_di_tam);
                            luu_vi_triAI = 2;
                            kiem_tra_hanh_dongAI = true;
                        }

                        if (id == 3)
                        {
                            van_dau = AI.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNangAI[3].Ten + "!";
                            KyNang(MangKyNangAI[3].Id, luot_di_tam);
                            luu_vi_triAI = 3;
                            kiem_tra_hanh_dongAI = true;
                        }

                        if (id == 4)
                        {
                            van_dau = AI.Ten + " vừa sử dụng thẻ kỹ năng " + MangKyNangAI[4].Ten + "!";
                            KyNang(MangKyNangAI[4].Id, luot_di_tam);
                            luu_vi_triAI = 4;
                            kiem_tra_hanh_dongAI = true;
                        }
                        if (id == 5)
                        {
                            van_dau = AI.Ten + " vừa sử dụng thẻ kỹ năng tối thượng " + AI.Ky_nang_toi_thuong + "!";
                            kiem_tra_ky_nangAI = false;
                            KyNangToiThuong(AI.Id, luot_di_tam);
                        }

                        if (suy_yeu_may == true) // Suy yếu
                            sat_thuong_may /= 2;

                        if (cuong_hoa_may == true) // Cường hóa
                            sat_thuong_may *= 2;

                        if (khien_bao_ve_nguoi_choi == true) // Khiên bảo vệ
                            sat_thuong_may = 0;

                        if (chuyen_doi_sat_thuong_may == true) // Chuyển đổi sát thương
                            Nguoi.Mau += sat_thuong_may;

                        if (giap_gai_nguoi_choi == false)
                        {
                            if (luot_di_tam == true && chuyen_doi_sat_thuong_nguoi_choi == false) // Đảo ngược
                                AI.Mau -= sat_thuong_nguoi_choi;
                            else if (luot_di_tam == false && chuyen_doi_sat_thuong_may == false)
                                Nguoi.Mau -= sat_thuong_may;
                        }    
                        else
                            AI.Mau -= sat_thuong_may;

                        if (ao_anh_may == true) // Ảo ảnh
                            ao_anh_may = false;

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
                else
                {
                    Console.ReadKey();
                    Thoat();
                }
            }
           
        }

        static int AILuaChon()
        {
            if (kiem_tra_ky_nangAI == true)
                return rd.Next(1, 6);
            else
                return rd.Next(1, 5);
        }

        static int NguoiLuaChon(string van_dau, bool kiem_tra_ky_nang)
        {
            int x_tam, y_tam;
            int x = 25, y = 20;
            Console.Clear();
            while(true)
            {
                GiaoDienMay(x, y, van_dau, kiem_tra_ky_nang);
                x_tam = x;
                y_tam = y;

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (y >= 22)
                            y -= 2;
                        break;

                    case ConsoleKey.DownArrow:
                        int vi_tri_y = 24;
                        if (kiem_tra_ky_nangAI == true)
                            vi_tri_y = 26;
                        if (y <= vi_tri_y)
                            y += 2;
                        break;

                    case ConsoleKey.P:
                        Console.Clear();
                        if (y == 20)
                            ThongTin(1);
                        if (y == 22)
                            ThongTin(2);
                        if (y == 24)
                            ThongTin(3);
                        if (y == 26)
                            ThongTin(4);
                        if (kiem_tra_ky_nang == true)
                        {
                            if (y == 28)
                                ThongTin(5);
                        }

                        Console.Clear();
                        break;

                    case ConsoleKey.Enter:
                        if (y == 20)
                            return 1;
                        if (y == 22)
                            return 2;
                        if (y == 24)
                            return 3;
                        if (y == 26)
                            return 4;
                        if (y == 28)
                            return 5;
                        break;
                }
                Console.SetCursorPosition(x_tam, y_tam);
                Console.Write("  ");
            }   
        }

        static void GiaoDienNguoiChoi(int x, int y, string van_dau, bool kiem_tra_ky_nang)
        {
            if (kiem_tra_bom_hen_gio_may == true)
            {
                Console.SetCursorPosition(31, 1);
                Console.Write("B - " + luot_cho_bom_hen_gio_may);
            }

            Console.SetCursorPosition(25, 2);
            if (kiem_tra_ky_nangAI == true)
                Console.Write("*");
            Console.WriteLine("Thẻ: " + AI.Ten + " Máu: " + AI.Mau + "/" + may_mau_toi_da + " Công: " + AI.Cong + " Thủ: " + AI.Thu);

            Console.SetCursorPosition(25, 10);
            Console.WriteLine(van_dau);

            if (kiem_tra_bom_hen_gio_nguoi_choi == true)
            {
                Console.SetCursorPosition(31, 17);
                Console.Write("B - " + luot_cho_bom_hen_gio_nguoi_choi);
            }

            Console.SetCursorPosition(25, 18);
            if (kiem_tra_ky_nang == true)
                Console.Write("*");
            Console.WriteLine("Thẻ: " + Nguoi.Ten + " Máu: " + Nguoi.Mau + "/" + nguoi_mau_toi_da + " Công: " + Nguoi.Cong + " Thủ: " + Nguoi.Thu);
            Console.SetCursorPosition(27, 20);
            if (ao_anh_nguoi_choi == false)
                Console.WriteLine(MangKyNang[1].Ten);
            else
                Console.WriteLine(AI.Ten);
            Console.SetCursorPosition(27, 22);
            if (ao_anh_nguoi_choi == false)
                Console.WriteLine(MangKyNang[2].Ten);
            else
                Console.WriteLine(AI.Ten);
            Console.SetCursorPosition(27, 24);
            if (ao_anh_nguoi_choi == false)
                Console.WriteLine(MangKyNang[3].Ten);
            else
                Console.WriteLine(AI.Ten);
            Console.SetCursorPosition(27, 26);
            if (ao_anh_nguoi_choi == false)
                Console.WriteLine(MangKyNang[4].Ten);
            else
                Console.WriteLine(AI.Ten);
            Console.SetCursorPosition(27, 28);
            if (kiem_tra_ky_nang == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(Nguoi.Ky_nang_toi_thuong);
                Console.ResetColor();
            }


            Console.SetCursorPosition(x, y);
            Console.Write("->");
        }

        static void GiaoDienMay(int x, int y, string van_dau, bool kiem_tra_ky_nang)
        {
            if (kiem_tra_bom_hen_gio_nguoi_choi == true)
            {
                Console.SetCursorPosition(31, 1);
                Console.Write("B - " + luot_cho_bom_hen_gio_nguoi_choi);
            }

            Console.SetCursorPosition(25, 2);
            if (kiem_tra_ky_nang == true)
                Console.Write("*");
            Console.WriteLine("Thẻ: " + Nguoi.Ten + " Máu: " + Nguoi.Mau + "/" + nguoi_mau_toi_da + " Công: " + Nguoi.Cong + " Thủ: " + Nguoi.Thu);

            Console.SetCursorPosition(25, 10);
            Console.WriteLine(van_dau);

            if (kiem_tra_bom_hen_gio_may == true)
            {
                Console.SetCursorPosition(31, 17);
                Console.Write("B - " + luot_cho_bom_hen_gio_may);
            }

            Console.SetCursorPosition(25, 18);
            if (kiem_tra_ky_nangAI == true)
                Console.Write("*");
            Console.WriteLine("Thẻ: " + AI.Ten + " Máu: " + AI.Mau + "/" + may_mau_toi_da + " Công: " + AI.Cong + " Thủ: " + AI.Thu);
            Console.SetCursorPosition(27, 20);
            if (ao_anh_may == false)
                Console.WriteLine(MangKyNangAI[1].Ten);
            else
                Console.WriteLine(Nguoi.Ten);
            Console.SetCursorPosition(27, 22);
            if (ao_anh_may == false)
                Console.WriteLine(MangKyNangAI[2].Ten);
            else
                Console.WriteLine(Nguoi.Ten);
            Console.SetCursorPosition(27, 24);
            if (ao_anh_may == false)
                Console.WriteLine(MangKyNangAI[3].Ten);
            else
                Console.WriteLine(Nguoi.Ten);
            Console.SetCursorPosition(27, 26);
            if (ao_anh_may == false)
                Console.WriteLine(MangKyNangAI[4].Ten);
            else
                Console.WriteLine(Nguoi.Ten);
            Console.SetCursorPosition(27, 28);
            if (kiem_tra_ky_nangAI == true)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write(AI.Ky_nang_toi_thuong);
                Console.ResetColor();
            }


            Console.SetCursorPosition(x, y);
            Console.Write("->");
        }

        static void ThongTin(int id)
        {
            while(true)
            {
                Console.SetCursorPosition(4, 5);
                if (ao_anh_may == false && ao_anh_nguoi_choi == false)
                {
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
                }
                else
                    Console.Write("???");
                Console.SetCursorPosition(4, 5);

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.Backspace:
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
                        sat_thuong_nguoi_choi = TanCong(Nguoi.Cong, AI.Thu);
                    else
                        sat_thuong_may = TanCong(AI.Cong, Nguoi.Thu);
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
                        sat_thuong_nguoi_choi = TanCongChiMang(Nguoi.Cong, AI.Thu);
                    else
                        sat_thuong_may = TanCongChiMang(AI.Cong, Nguoi.Thu);
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
                case 11:
                    if (luot_di == true)
                    {
                        luu_giap_nguoi_choi = Nguoi.Thu;
                        Nguoi.Thu = GiapAo(Nguoi.Thu);
                        tang_giap_nguoi_choi = true;
                        luu_dem_giap_ao_nguoi_choi = 2;
                    }
                    else
                    {
                        luu_giap_may = AI.Thu;
                        AI.Thu = GiapAo(AI.Thu);
                        tang_giap_may = true;
                        luu_dem_giap_ao_may = 2;
                    }
                    break;
                case 12:
                    if (luot_di == true)
                    {
                        luu_mau_ao_nguoi_choi = Nguoi.Mau;
                        Nguoi.Mau = MauAo(nguoi_mau_toi_da);
                        tang_mau_ao_nguoi_choi = true;
                        luu_dem_mau_ao_nguoi_choi = 2;
                    }
                    else
                    {
                        luu_mau_ao_may = AI.Mau;
                        AI.Mau = MauAo(may_mau_toi_da);
                        tang_mau_ao_may = true;
                        luu_dem_mau_ao_may = 2;
                    }
                    break;
                case 13:
                    if (luot_di == true)
                    {
                        sat_thuong_nguoi_choi = TanCongTrongKich(Nguoi.Cong, AI.Thu);
                        if (AI.Thu > 0)
                            AI.Thu--;
                    }
                    else
                    {
                        sat_thuong_may = TanCongTrongKich(AI.Cong, Nguoi.Thu);
                        if (Nguoi.Thu > 0)
                            Nguoi.Thu--;
                    }
                    break;
                case 14:
                    if (luot_di == true)                   
                        sat_thuong_nguoi_choi = SatThuongCuoiCung(Nguoi.Cong, AI.Thu, Nguoi.Mau, nguoi_mau_toi_da); 
                    else                    
                        sat_thuong_may = SatThuongCuoiCung(AI.Cong, Nguoi.Thu, AI.Mau, may_mau_toi_da);                        
                    break;
                case 15:
                    if (luot_di == true)
                    {
                        int mau_truoc_khi_danh = AI.Mau;
                        sat_thuong_nguoi_choi = TanCongPhucHoi(Nguoi.Cong, AI.Thu);
                        if (khien_bao_ve_may == false)
                        {
                            Nguoi.Mau += mau_truoc_khi_danh - (AI.Mau - sat_thuong_nguoi_choi);
                            if (Nguoi.Mau >= nguoi_mau_toi_da)
                                Nguoi.Mau = nguoi_mau_toi_da;
                        }      
                    }
                    else
                    {
                        int mau_truoc_khi_danh = Nguoi.Mau;
                        sat_thuong_may = TanCongPhucHoi(AI.Cong, Nguoi.Thu);
                        if (khien_bao_ve_nguoi_choi == false)
                        {
                            AI.Mau += mau_truoc_khi_danh - (Nguoi.Mau - sat_thuong_may);
                            if (AI.Mau >= may_mau_toi_da)
                                AI.Mau = may_mau_toi_da;
                        }
                    }                  
                    break;
                case 16:
                    if (luot_di == true)
                    {
                        KhienBaoVe();
                        khien_bao_ve_nguoi_choi = true;
                        luu_dem_khien_bao_ve_nguoi_choi = 2;
                    }
                    else
                    {
                        KhienBaoVe();
                        khien_bao_ve_may = true;
                        luu_dem_khien_bao_ve_may = 2;
                    }
                    break;
                case 17:
                    if (luot_di == true)
                    {
                        DaoNguoc();
                        dao_nguoc_may = true;                       
                    }
                    else
                    {
                        DaoNguoc();
                        dao_nguoc_nguoi_choi = true;
                    }
                    break;
                case 18:
                    if (luot_di == true)
                    {
                        HienThucHoa();
                        hien_thuc_hoa = true;
                    }
                    else
                    {
                        HienThucHoa();
                        hien_thuc_hoa = true;
                    }
                    break;
                case 19:
                    if (luot_di == true)
                    {
                        SongAnhChiSo();
                        Nguoi.Mau = AI.Mau;
                        Nguoi.Cong = AI.Cong;
                        Nguoi.Thu = AI.Thu;
                    }
                    else
                    {
                        SongAnhChiSo();
                        AI.Mau = Nguoi.Mau;
                        AI.Cong = Nguoi.Cong;
                        AI.Thu = Nguoi.Thu;
                    }
                    break;
                case 20:
                    if (luot_di == true)
                    {
                        BomHenGio();
                        string thoi_luong;
                        string luu_chuoi_ky_nang;
                        do
                        {
                            Console.SetCursorPosition(25, 10);
                            luu_chuoi_ky_nang = "Thẻ Bom Hẹn Giờ - Vui lòng nhập số lượt để bom phát nổ: ";
                            Console.Write(luu_chuoi_ky_nang);
                            thoi_luong = Console.ReadLine();
                            Console.SetCursorPosition(25, 10);
                            for (int i = 0; i < luu_chuoi_ky_nang.Length + thoi_luong.Length; i++)
                                Console.Write(" ");
                        } while (int.Parse(thoi_luong) < 1);                        
                        luot_cho_bom_hen_gio_may = int.Parse(thoi_luong); // ép kiểu
                        kiem_tra_bom_hen_gio_may = true;                     
                    }
                    else
                    {
                        BomHenGio();
                        if (choi_co_op == false && dao_nguoc_nguoi_choi == false)
                        {
                            luot_cho_bom_hen_gio_nguoi_choi = rd.Next(1, 6);
                            kiem_tra_bom_hen_gio_nguoi_choi = true;
                        }    
                        else if(choi_co_op == true || dao_nguoc_nguoi_choi == true)
                        {
                            string thoi_luong;
                            string luu_chuoi_ky_nang;
                            do
                            {
                                Console.SetCursorPosition(25, 10);
                                luu_chuoi_ky_nang = "Thẻ Bom Hẹn Giờ - Vui lòng nhập số lượt để bom phát nổ: ";
                                Console.Write(luu_chuoi_ky_nang);
                                thoi_luong = Console.ReadLine();
                                Console.SetCursorPosition(25, 10);
                                for (int i = 0; i < luu_chuoi_ky_nang.Length + thoi_luong.Length; i++)
                                    Console.Write(" ");
                            } while (int.Parse(thoi_luong) < 1);
                            luot_cho_bom_hen_gio_nguoi_choi = int.Parse(thoi_luong); // ép kiểu
                            kiem_tra_bom_hen_gio_nguoi_choi = true;
                        }    
                    }
                    break;
                case 21:
                    if (luot_di == true)
                    {
                        CuongHoa();
                        cuong_hoa_nguoi_choi = true;
                        luu_dem_cuong_hoa_nguoi_choi = 2;
                    }
                    else
                    {
                        CuongHoa();
                        cuong_hoa_may = true;
                        luu_dem_cuong_hoa_may = 2;
                    }
                    break;
                case 22:
                    if (luot_di == true)
                    {
                        SuyYeu();
                        suy_yeu_may = true;
                        luu_dem_suy_yeu_may = 2;
                    }
                    else
                    {
                        SuyYeu();
                        suy_yeu_nguoi_choi = true;
                        luu_dem_suy_yeu_nguoi_choi = 2;
                    }
                    break;
                case 23:
                    if (luot_di == true)
                    {
                        AoAnh();
                        ao_anh_may = true;
                    }
                    else
                    {
                        AoAnh();
                        ao_anh_nguoi_choi = true;
                    }
                    break;
                case 24:
                    if (luot_di == true)
                    {
                        HoanDoiChiSo();
                        int[] MangChiSo = new int[100];
                        MangChiSo[1] = Nguoi.Mau;
                        MangChiSo[2] = Nguoi.Cong;
                        MangChiSo[3] = Nguoi.Thu;

                        List<int> chi_so = NgauNhien(1, 4);

                        int dem_chi_so = 0;
                        foreach (int chi_so_nguoi in chi_so)
                        {
                            dem_chi_so++;
                            switch (chi_so_nguoi)
                            {
                                case 1:
                                    Nguoi.Mau = MangChiSo[dem_chi_so];
                                    break;
                                case 2:
                                    Nguoi.Cong = MangChiSo[dem_chi_so];
                                    break;
                                case 3:
                                    Nguoi.Thu = MangChiSo[dem_chi_so];
                                    break;
                            }
                        }
                    }
                    else
                    {
                        HoanDoiChiSo();
                        int[] MangChiSoAI = new int[100];
                        MangChiSoAI[1] = AI.Mau;
                        MangChiSoAI[2] = AI.Cong;
                        MangChiSoAI[3] = AI.Thu;

                        List<int> chi_so = NgauNhien(1, 4);

                        int dem_chi_so = 0;
                        foreach (int chi_so_may in chi_so)
                        {
                            dem_chi_so++;
                            switch (chi_so_may)
                            {
                                case 1:
                                    AI.Mau = MangChiSoAI[dem_chi_so];
                                    break;
                                case 2:
                                    AI.Cong = MangChiSoAI[dem_chi_so];
                                    break;
                                case 3:
                                    AI.Thu = MangChiSoAI[dem_chi_so];
                                    break;
                            }
                        }

                    }
                    break;
                case 25:
                    if (luot_di == true)                  
                        sat_thuong_nguoi_choi = TanCongXuyenGiap(Nguoi.Cong);
                    else
                        sat_thuong_may = TanCongXuyenGiap(AI.Cong);
                    break;
                case 26:
                    if (luot_di == true)
                    {
                        ChuyenDoiSatThuong();
                        chuyen_doi_sat_thuong_may = true;
                        luu_dem_chuyen_doi_sat_thuong_may = 2;
                    }
                    else
                    {
                        ChuyenDoiSatThuong();
                        chuyen_doi_sat_thuong_nguoi_choi = true;
                        luu_dem_chuyen_doi_sat_thuong_nguoi_choi = 2;
                    }
                    break;
                case 27:
                    if (luot_di == true)
                    {
                        DoiCho();
                        int mau_tam = nguoi_mau_toi_da;
                        nguoi_mau_toi_da = Nguoi.Mau;
                        Nguoi.Mau = mau_tam;
                    }
                    else
                    {
                        DoiCho();
                        int mau_tam = may_mau_toi_da;
                        may_mau_toi_da = AI.Mau;
                        AI.Mau = mau_tam;
                    }
                    break;
                case 28:
                    if (luot_di == true)
                    {
                        GiapGai();
                        giap_gai_nguoi_choi = true;
                        luu_dem_giap_gai_nguoi_choi = 2;
                    }
                    else
                    {
                        GiapGai();
                        giap_gai_may = true;
                        luu_dem_giap_gai_may = 2;
                    }
                    break;
                case 29:
                    if (luot_di == true)
                    {
                        QuayRoi();
                        quay_roi_may = true;
                    }
                    else
                    {
                        QuayRoi();
                        quay_roi_nguoi_choi = true;
                    }
                    break;
                case 30:
                    if (luot_di == true)
                    {
                        EpBuoc();
                        ep_buoc_may = true;
                    }
                    else
                    {
                        EpBuoc();
                        ep_buoc_nguoi_choi = true;
                    }
                    break;
                case 31:
                    if (luot_di == true)
                        may_mau_toi_da = LoiKeoCuaTuThan(may_mau_toi_da);
                    else
                        nguoi_mau_toi_da = LoiKeoCuaTuThan(nguoi_mau_toi_da);

                    if (may_mau_toi_da <= 0)
                        may_mau_toi_da = 0;
                    if (nguoi_mau_toi_da <= 0)
                        nguoi_mau_toi_da = 0;
                    break;
                case 32:
                    if (luot_di == true)
                        AI.Thu = XienGiap(AI.Thu);                    
                    else
                        Nguoi.Thu = XienGiap(Nguoi.Thu);

                    if (AI.Thu <= 0)
                        AI.Thu = 0;
                    if (Nguoi.Thu <= 0)
                        Nguoi.Thu = 0;
                    break;
                case 33:
                    if (luot_di == true)
                        AI.Cong = SuyGiam(AI.Cong);               
                    else
                        Nguoi.Cong = SuyGiam(Nguoi.Cong);

                    if (AI.Cong <= 0)
                        AI.Cong = 0;
                    if (Nguoi.Cong <= 0)
                        Nguoi.Cong = 0;
                    break;
                case 34:
                    if (luot_di == true)
                    {
                        ChuyenDoiKyNang();
                        string ky_nang;
                        string luu_chuoi_ky_nang;
                        int id_ky_nang_may;                        
                        do
                        {
                            Console.SetCursorPosition(25, 10);
                            luu_chuoi_ky_nang = "Vui lòng nhập vị trí kỹ năng hiện có để chuyển đổi: ";
                            Console.Write(luu_chuoi_ky_nang);
                            ky_nang = Console.ReadLine();
                            Console.SetCursorPosition(25, 10);
                            for (int i = 0; i < luu_chuoi_ky_nang.Length + ky_nang.Length; i++)
                                Console.Write(" ");                           
                        } while (int.Parse(ky_nang) > 4 || int.Parse(ky_nang) < 1);

                        id_ky_nang_may = rd.Next(1, 5);
                        TheKyNang KyNangTam = new TheKyNang();
                        KyNangTam = MangKyNangAI[id_ky_nang_may];
                        MangKyNangAI[id_ky_nang_may] = MangKyNang[int.Parse(ky_nang)];
                        MangKyNang[int.Parse(ky_nang)] = KyNangTam;

                    }
                    else
                    {
                        ChuyenDoiKyNang();
                        int id_ky_nang_nguoi_choi;
                        int id_ky_nang_may;
                        if (choi_co_op == false && dao_nguoc_nguoi_choi == false)
                        {
                            id_ky_nang_nguoi_choi = rd.Next(1, 5);
                            id_ky_nang_may = rd.Next(1, 5);
                            TheKyNang KyNangTam = new TheKyNang();
                            KyNangTam = MangKyNangAI[id_ky_nang_may];
                            MangKyNangAI[id_ky_nang_may] = MangKyNang[id_ky_nang_nguoi_choi];
                            MangKyNang[id_ky_nang_nguoi_choi] = KyNangTam;
                        }
                        else if (choi_co_op == true || dao_nguoc_nguoi_choi == true)
                        {
                            string ky_nang;
                            string luu_chuoi_ky_nang;
                            do
                            {
                                Console.SetCursorPosition(25, 10);
                                luu_chuoi_ky_nang = "Vui lòng nhập vị trí kỹ năng hiện có để chuyển đổi: ";
                                Console.Write(luu_chuoi_ky_nang);
                                ky_nang = Console.ReadLine();
                                Console.SetCursorPosition(25, 10);
                                for (int i = 0; i < luu_chuoi_ky_nang.Length + ky_nang.Length; i++)
                                    Console.Write(" ");
                            } while (int.Parse(ky_nang) > 4 || int.Parse(ky_nang) < 1);

                            id_ky_nang_nguoi_choi = rd.Next(1, 5);
                            TheKyNang KyNangTam = new TheKyNang();
                            KyNangTam = MangKyNangAI[int.Parse(ky_nang)];
                            MangKyNangAI[int.Parse(ky_nang)] = MangKyNang[id_ky_nang_nguoi_choi];
                            MangKyNang[id_ky_nang_nguoi_choi] = KyNangTam;
                        }

                    }    
                    break;
                case 35:
                    if (luot_di == true)
                    {
                        LuaChonMaQuai();
                        Console.Clear();
                        TheKyNang ky_nang_tam = DanhSachKyNang();
                        MangKyNang[luu_vi_tri] = ky_nang_tam;
                        kiem_tra_hanh_dong = false;
                        GiaoDienNguoiChoi(x_giao_dien_nguoi_choi_tam, y_giao_dien_nguoi_choi_tam, van_dau_giao_dien_nguoi_choi_tam, kiem_tra_ky_nang_giao_dien_nguoi_choi_tam);
                    }
                    else
                    {
                        LuaChonMaQuai();
                        TheKyNang ky_nang_tam;
                        if (choi_co_op == false && dao_nguoc_nguoi_choi == false)
                        {
                            int id_lua_chon_ma_quai_may = rd.Next(1, so_luong_ky_nang - 4);
                            ky_nang_tam = LuuKyNangAI[id_lua_chon_ma_quai_may];
                            MangKyNangAI[luu_vi_triAI] = ky_nang_tam;
                            kiem_tra_hanh_dongAI = false;
                        }
                        else if (choi_co_op == true || dao_nguoc_nguoi_choi == true)
                        {
                            Console.Clear();
                            ky_nang_tam = DanhSachKyNang();
                            MangKyNangAI[luu_vi_triAI] = ky_nang_tam;
                            kiem_tra_hanh_dongAI = false;
                            if (dao_nguoc_nguoi_choi == true)
                                GiaoDienNguoiChoi(x_giao_dien_nguoi_choi_tam, y_giao_dien_nguoi_choi_tam, van_dau_giao_dien_nguoi_choi_tam, kiem_tra_ky_nang_giao_dien_nguoi_choi_tam);
                            if (choi_co_op == true)
                                GiaoDienMay(x_giao_dien_may_tam, y_giao_dien_may_tam, van_dau_giao_dien_may_tam, kiem_tra_ky_nang_giao_dien_may_tam);
                        }
                    }
                    break;
                case 36:
                    if (luot_di == true)
                    {
                        TanCongQuyDoi();
                        string suc_tan_cong;
                        string luu_chuoi_ky_nang;
                        do
                        {
                            Console.SetCursorPosition(25, 10);
                            luu_chuoi_ky_nang = "Vui lòng nhập lượng máu để quy đổi: ";
                            Console.Write(luu_chuoi_ky_nang);
                            suc_tan_cong = Console.ReadLine();
                            Console.SetCursorPosition(25, 10);
                            for (int i = 0; i < luu_chuoi_ky_nang.Length + suc_tan_cong.Length; i++)
                                Console.Write(" ");
                        } while (int.Parse(suc_tan_cong) < 0 || int.Parse(suc_tan_cong) > Nguoi.Mau);
                        sat_thuong_nguoi_choi = int.Parse(suc_tan_cong);
                        sat_thuong_nguoi_choi -= AI.Thu;
                        Nguoi.Mau -= int.Parse(suc_tan_cong);
                    }
                    else
                    {
                        TanCongQuyDoi();
                        if (choi_co_op == false && dao_nguoc_nguoi_choi == false)
                        {
                            int suc_tan_cong_may = rd.Next(1, AI.Mau);
                            sat_thuong_may = suc_tan_cong_may;
                            sat_thuong_may -= Nguoi.Thu;
                            AI.Mau -= suc_tan_cong_may;
                        }
                        else if (choi_co_op == true || dao_nguoc_nguoi_choi == true)
                        {
                            string suc_tan_cong_may;
                            string luu_chuoi_ky_nang;
                            do
                            {
                                Console.SetCursorPosition(25, 10);
                                luu_chuoi_ky_nang = "Vui lòng nhập lượng máu để quy đổi: ";
                                Console.Write(luu_chuoi_ky_nang);
                                suc_tan_cong_may = Console.ReadLine();
                                Console.SetCursorPosition(25, 10);
                                for (int i = 0; i < luu_chuoi_ky_nang.Length + suc_tan_cong_may.Length; i++)
                                    Console.Write(" ");
                            } while (int.Parse(suc_tan_cong_may) < 0 || int.Parse(suc_tan_cong_may) > AI.Mau);
                            sat_thuong_may = int.Parse(suc_tan_cong_may);
                            sat_thuong_may -= Nguoi.Thu;
                            AI.Mau -= int.Parse(suc_tan_cong_may);
                        }
                    }
                    break;
                case 37:
                    if (luot_di == true)
                    {
                        sat_thuong_nguoi_choi = TanCongXuyenPha(Nguoi.Cong, AI.Thu);
                        Nguoi.Mau -= sat_thuong_nguoi_choi;
                    }                           
                    else
                    {
                        sat_thuong_may = TanCongXuyenPha(AI.Cong, Nguoi.Thu);
                        AI.Mau -= sat_thuong_may;
                    }                           
                    break;
            }
        }

        static TheKyNang DanhSachKyNang()
        {
            int x = 5, y = 7;
            int x_tam, y_tam;
            int x_lap, y_lap;
            int trang = 1, so_trang = (so_luong_ky_nang - 4) / 5;
            int hien_thi_ky_nang = 1;

            while (true)
            {
                Console.Clear();
                
                x_lap = 7;
                y_lap = 7;
                Console.SetCursorPosition(5, 5);
                Console.WriteLine("Danh Sách Kỹ Năng");
                for(int i = hien_thi_ky_nang; i <= hien_thi_ky_nang+4; i++)
                {
                    Console.SetCursorPosition(x_lap, y_lap);
                    Console.WriteLine(LuuKyNang[i].Ten);
                    y_lap += 2;
                }
                Console.SetCursorPosition(5, 17);
                Console.WriteLine("<"+trang + "/" + so_trang+">");

                Console.SetCursorPosition(x, y);
                Console.Write("->");


                x_tam = x;
                y_tam = y;

                ConsoleKeyInfo lua_chon = Console.ReadKey();

                switch (lua_chon.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (y >= 9)
                            y -= 2;
                        break;

                    case ConsoleKey.DownArrow:
                        if (y <= 13)
                            y += 2;
                        break;

                    case ConsoleKey.LeftArrow:
                        if (trang > 1)
                        {
                            trang--;
                            hien_thi_ky_nang -= 5;
                        }           
                        break;

                    case ConsoleKey.RightArrow:
                        if (trang < so_trang)
                        {
                            trang++;
                            hien_thi_ky_nang += 5;
                        }                              
                        break;

                    case ConsoleKey.Enter:
                        Console.Clear();
                        if (y == 7)
                            return LuuKyNang[hien_thi_ky_nang];
                        if (y == 9)
                            return LuuKyNang[hien_thi_ky_nang+1];
                        if (y == 11)
                            return LuuKyNang[hien_thi_ky_nang+2];
                        if (y == 13)
                            return LuuKyNang[hien_thi_ky_nang+3];
                        if (y == 15)
                            return LuuKyNang[hien_thi_ky_nang+4];
                        break;

                }

                Console.SetCursorPosition(x_tam, y_tam);
                Console.Write("  ");
            }
        }

        static void KyNangToiThuong(int id, bool luot_di)
        {
            switch (id)
            {
                case 1:
                    if (luot_di == true)
                        sat_thuong_nguoi_choi = PhaoHoaNguc(Nguoi.Cong, AI.Thu);
                    else
                        sat_thuong_may = PhaoHoaNguc(AI.Cong, Nguoi.Thu);
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
            if (hien_thuc_hoa == false)
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

                    if (tang_giap_may == true) // 2 lượt
                    {
                        if (luu_dem_giap_ao_may == 1)
                        {
                            tang_giap_may = false;
                            AI.Thu = luu_giap_may;
                        }
                        else
                            luu_dem_giap_ao_may--;
                    }

                    if (tang_mau_ao_may == true) // 2 lượt
                    {
                        if (luu_dem_mau_ao_may == 1)
                        {
                            tang_mau_ao_may = false;
                            AI.Mau = luu_mau_ao_may;
                        }
                        else
                            luu_dem_mau_ao_may--;
                    }

                    if (khien_bao_ve_may == true) // 2 lượt
                    {
                        if (luu_dem_khien_bao_ve_may == 1)
                            khien_bao_ve_may = false;
                        else
                            luu_dem_khien_bao_ve_may--;
                    }

                    if (dao_nguoc_may == true) // 1 lượt thì bình thường                
                        dao_nguoc_may = false;
                    
                    if (kiem_tra_bom_hen_gio_may == true)
                    {
                        if (luot_cho_bom_hen_gio_may == 1)
                        {
                            kiem_tra_bom_hen_gio_may = false;
                            AI.Mau -= Nguoi.Cong/2;
                        }
                        else
                            luot_cho_bom_hen_gio_may--;
                    }

                    if (cuong_hoa_may == true) // 2 lượt
                    {
                        if (luu_dem_cuong_hoa_may == 1)                        
                            cuong_hoa_may = false;                       
                        else
                            luu_dem_cuong_hoa_may--;
                    }

                    if (suy_yeu_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_suy_yeu_nguoi_choi == 1)
                            suy_yeu_nguoi_choi = false;
                        else
                            luu_dem_suy_yeu_nguoi_choi--;
                    }
                    
                    if (chuyen_doi_sat_thuong_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_chuyen_doi_sat_thuong_nguoi_choi == 1)
                        {
                            chuyen_doi_sat_thuong_nguoi_choi = false;
                        }
                        else
                            luu_dem_chuyen_doi_sat_thuong_nguoi_choi--;
                    }    

                    if (giap_gai_may == true) // 2 lượt
                    {
                        if (luu_dem_giap_gai_may == 1)
                        {
                            giap_gai_may = false;
                        }
                        else
                            luu_dem_giap_gai_may--;
                    }

                    if (quay_roi_may == true) // 1 lượt thì bình thường                
                        quay_roi_may = false;

                    if (ep_buoc_may == true) // 1 lượt thì bình thường                
                        ep_buoc_may = false;

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

                    if (tang_giap_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_giap_ao_nguoi_choi == 1)
                        {
                            tang_giap_nguoi_choi = false;
                            Nguoi.Thu = luu_giap_nguoi_choi;
                        }
                        else
                            luu_dem_giap_ao_nguoi_choi--;
                    }

                    if (tang_mau_ao_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_mau_ao_nguoi_choi == 1)
                        {
                            tang_mau_ao_nguoi_choi = false;
                            Nguoi.Mau = luu_mau_ao_nguoi_choi;
                        }
                        else
                            luu_dem_mau_ao_nguoi_choi--;
                    }

                    if (khien_bao_ve_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_khien_bao_ve_nguoi_choi == 1)
                            khien_bao_ve_nguoi_choi = false;
                        else
                            luu_dem_khien_bao_ve_nguoi_choi--;
                    }

                    if (dao_nguoc_nguoi_choi == true) // 1 lượt thì bình thường                
                        dao_nguoc_nguoi_choi = false;

                    if (kiem_tra_bom_hen_gio_nguoi_choi == true)
                    {
                        if (luot_cho_bom_hen_gio_nguoi_choi == 1)
                        {
                            kiem_tra_bom_hen_gio_nguoi_choi = false;
                            Nguoi.Mau -= AI.Cong/2;
                        }
                        else
                            luot_cho_bom_hen_gio_nguoi_choi--;
                    }

                    if (cuong_hoa_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_cuong_hoa_nguoi_choi == 1)
                            cuong_hoa_nguoi_choi = false;
                        else
                            luu_dem_cuong_hoa_nguoi_choi--;
                    }

                    if (suy_yeu_may == true) // 2 lượt
                    {
                        if (luu_dem_suy_yeu_may == 1)
                            suy_yeu_may = false;
                        else
                            luu_dem_suy_yeu_may--;
                    }

                    if (chuyen_doi_sat_thuong_may == true) // 2 lượt
                    {
                        if (luu_dem_chuyen_doi_sat_thuong_may == 1)
                        {
                            chuyen_doi_sat_thuong_may = false;
                        }
                        else
                            luu_dem_chuyen_doi_sat_thuong_may--;
                    }

                    if (giap_gai_nguoi_choi == true) // 2 lượt
                    {
                        if (luu_dem_giap_gai_nguoi_choi == 1)
                        {
                            giap_gai_nguoi_choi = false;
                        }
                        else
                            luu_dem_giap_gai_nguoi_choi--;
                    }

                    if (quay_roi_nguoi_choi == true) // 1 lượt thì bình thường                
                        quay_roi_nguoi_choi = false;

                    if (ep_buoc_nguoi_choi == true) // 1 lượt thì bình thường                
                        ep_buoc_nguoi_choi = false;
                }
            }    
            else // Vô hiệu trạng thái ảo
            {
                hien_thuc_hoa = false;
                giam_cong_may = false;
                giam_cong_nguoi_choi = false;
                giam_thu_may = false;
                giam_thu_nguoi_choi = false;
                tang_cong_may = false;
                tang_cong_nguoi_choi = false;
                tang_giap_may = false;
                tang_giap_nguoi_choi = false;
                tang_mau_ao_may = false;
                tang_mau_ao_nguoi_choi = false;
            }    
            
        }

    }
}
