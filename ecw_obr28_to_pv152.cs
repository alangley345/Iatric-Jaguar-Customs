//Uses Jaguar's boiler plate to try below code and return a empty string is their is an exception.
public string obr28_pv152(string arg1 string arg2){
    try{       
        //function overwrites 2nd repeat in OBR-28 with contents of PV1-52
        //arg1 = PV1-52
        //arg2 = OBR-28

        string newString = "";
        arg2             = arg2 + "~~~";
        string[] subVals = arg2.Split('~');
        if (arg1 != (""))
        {
                newString = subVals[0] + "~" + arg1 + "~" + subVals[2] + "~" + subVals[3];
        }
        
        else
        {
                newString = subVals[0] + "~" + subVals[1];
        }
        return newString.Trim('~')

    }

    catch (Exception ex){
        return string.Empty;
    }
}
