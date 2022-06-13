//Uses Jaguar's boiler plate to try below code and return a empty string is their is an exception.
public string replace_canadian_addresses(string arg1)
{
    try{
        string address  = "214^KING STREET^OGDENSBURG^NY^13669";
        string[] pid_11_0 = arg1.Split('^');
        StringBuilder temp = new StringBuilder();
        temp.Append(pid_11_0[pid_11_0.Length - 1]);
        string zip = temp.ToString();
        //regex expression here
        string test     = "^[0-9]*$";
        
        if (System.Text.RegularExpressions.Regex.IsMatch(zip, test))
        {
            return arg1;
        }

        else
        {
            return address;
        }
    }
    
    catch (Exception ex){
    return string.Empty;
    }
}
