using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDOperations
{
    public class Response
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public string? Data { get; set; }
    }

    public class Response<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }


    

    public class ResponseList<T>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<T>? Data { get; set; }
        public int TotalRecords { get; set; }
        public int RecordsFiltered { get; set; }

    }



    public class ResponseBindListMulti<T1, T2>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public T1? List1 { get; set; }
        public List<T2>? List2 { get; set; }

    }
    public class ResponseBindListMulti1<T1, T2>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<T1>? List1 { get; set; }
        public List<T2>? List2 { get; set; }

    }
    public class ResponseBindListMulti1<T1, T2, T3>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<T1>? List1 { get; set; }
        public List<T2>? List2 { get; set; }
        public List<T3>? List3 { get; set; }
        

    }
    public class ResponseBindListMulti1<T1, T2, T3, T4>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<T1>? List1 { get; set; }
        public List<T2>? List2 { get; set; }
        public List<T3>? List3 { get; set; }
        public List<T4>? List4 { get; set; }
        

    }
    public class ResponseBindListMulti1<T1, T2, T3, T4, T5>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<T1>? List1 { get; set; }
        public List<T2>? List2 { get; set; }
        public List<T3>? List3 { get; set; }
        public List<T4>? List4 { get; set; }
        public List<T5>? List5 { get; set; }
        


    }
    public class ResponseBindDropdownListMulti<T1, T2, T3, T4, T5, T6>
    {
        public bool Status { get; set; }
        public string? Message { get; set; }
        public List<T1>? List1 { get; set; }
        public List<T2>? List2 { get; set; }
        public List<T3>? List3 { get; set; }
        public List<T4>? List4 { get; set; }
        public List<T5>? List5 { get; set; }
        public List<T6>? List6 { get; set; }
    }


}
