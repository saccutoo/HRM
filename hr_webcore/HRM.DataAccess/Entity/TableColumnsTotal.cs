using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRM.DataAccess.Entity
{
    public class TableColumnsTotal
    {
        #region ------------ BIẾN TỔNG --------------------
        public string Total1 { set; get; }
        public string Total2 { set; get; }
        public string Total3 { set; get; }
        public string Total4 { set; get; }
        public string Total5 { set; get; }
        public string Total6 { set; get; }
        public string Total7 { set; get; }
        public string Total8 { set; get; }
        public string Total9 { set; get; }
        public string Total10 { set; get; }
        public string Total11 { set; get; }
        public string Total12 { set; get; }
        public string Total13 { set; get; }
        public string Total14 { set; get; }
        public string Total15 { set; get; }
        public string Total16 { set; get; }
        public string Total17 { set; get; }
        public string Total18 { set; get; }
        public string Total19 { set; get; }
        public string Total20 { set; get; }
        public string Total21 { set; get; }
        public string Total22 { set; get; }
        public string Total23 { set; get; }
        public string Total24 { set; get; }
        public string Total25 { set; get; }
        public string Total26 { set; get; }
        public string Total27 { set; get; }
        public string Total28 { set; get; }
        public string Total29 { set; get; }
        public string Total30 { set; get; }
        public string Total31 { set; get; }
        public string Total32 { set; get; }
        public string Total33 { set; get; }
        public string Total34 { set; get; }
        public string Total35 { set; get; }
        public string Total36 { set; get; }
        public string Total37 { set; get; }
        public string Total38 { set; get; }
        public string Total39 { set; get; }
        public string Total40 { set; get; }
        public string Total41 { set; get; }
        public string Total42 { set; get; }
        public string Total43 { set; get; }
        public string Total44 { set; get; }
        public string Total45 { set; get; }
        public string TotalQ1 { set; get; }
        public string TotalQ2 { set; get; }
        public string TotalQ3 { set; get; }
        public string TotalQ4 { set; get; }
        public string TotalQuaterSum { get; set; }
        public int ClientManager { set; get; }
        public int ClientLimit { set; get; }
        #endregion ----------------------------------
        #region ------------ BIẾN BenchMark --------------------
        public string Benchmark1 { set; get; }
        public string Benchmark2 { set; get; }
        public string Benchmark3 { set; get; }
        public string Benchmark4 { set; get; }
        public string Benchmark5 { set; get; }
        public string Benchmark6 { set; get; }
        public string Benchmark7 { set; get; }
        public string Benchmark8 { set; get; }
        public string Benchmark9 { set; get; }
        public string Benchmark10 { set; get; }
        public string Benchmark11 { set; get; }
        public string Benchmark12 { set; get; }
        public string Benchmark13 { set; get; }
        public string Benchmark14 { set; get; }
        public string Benchmark15 { set; get; }
        public string Benchmark16 { set; get; }
        public string Benchmark17 { set; get; }
        public string Benchmark18 { set; get; }
        public string Benchmark19 { set; get; }
        public string Benchmark20 { set; get; }
        public string Benchmark21 { set; get; }
        public string Benchmark22 { set; get; }
        public string Benchmark23 { set; get; }
        public string Benchmark24 { set; get; }
        public string Benchmark25 { set; get; }
        public string Benchmark26 { set; get; }
        public string Benchmark27 { set; get; }
        public string Benchmark28 { set; get; }
        public string Benchmark29 { set; get; }
        public string Benchmark30 { set; get; }
        public string Benchmark31 { set; get; }
        public string Benchmark32 { set; get; }
        public string Benchmark33 { set; get; }
        public string Benchmark34 { set; get; }
        public string Benchmark35 { set; get; }
        public string Benchmark36 { set; get; }
        public string Benchmark37 { set; get; }
        public string Benchmark38 { set; get; }
        public string Benchmark39 { set; get; }
        public string Benchmark40 { set; get; }
        public string Benchmark41 { set; get; }
        public string Benchmark42 { set; get; }
        public string Benchmark43 { set; get; }
        public string Benchmark44 { set; get; }
        public string Benchmark45 { set; get; }
        #endregion ----------------------------------

        /// <summary>
        /// Trang hiện tại
        /// </summary>
        public int PageIndex { set; get; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int PageCount { set; get; }

        /// <summary>
        /// Bản ghi trên trang hiện tại
        /// </summary>
        public int Elements { set; get; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public int TotalRecords { set; get; }

        /// <summary>
        /// Số lượng bản ghi trên một trang cần hiển thị
        /// </summary>
        public int PageSize { set; get; }


        /// <summary>
        /// Tên Controller
        /// </summary>
        public string ControllerName { set; get; }
        /// <summary>
        /// Tên Div chứa Grid
        /// </summary>
        public string GridName { set; get; }
        /// <summary>
        /// Sự kiện lấy danh sách
        /// </summary>
        public string ActionName { set; get; }

        /// Cho phép Edit hay không
        /// </summary>
        public int IsAllowEdit { set; get; }

        public string TypeOrganization { get; set; }
    }
}
