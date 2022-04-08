//Uses Jaguar's boiler plate to try below code and return a empty string is their is an exception.
public string CalculateAge(string arg1){
    try{       
        int age         = 0; 
        string format   = "yyyyMMdd";
        age             = (DateTime.Today.Subtract(DateTime.ParseExact(arg1, format, null))).Days;
        age             = age / 365;  
        return age.ToString();  
    }
    catch (Exception ex){
        return string.Empty;
    }
}
