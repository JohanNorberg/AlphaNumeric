# AlphaNumeric
This project contains two ways of encoding data or string to only use alpha-numeric characters.

All encoded strings should be `a-z A-Z 0-9`

# Encode Data
Encode raw data using the data-encoder.

    public void EncodeData()
    {
        byte[] originalByteArray = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }; // Raw Data
        string alphaNumericEncodedString = AlphaNumeric.Data.EncodeBytes(originalByteArray); //Output is AAECAwQFBgcICQ0404
        byte[] originalBytes = AlphaNumeric.Data.DecodeBytes(alphaNumericEncodedString); // Gets the original bytes back
    }
    
Encode raw string using the data-encoder.
    
    public void EncodeStringData()
    {
        string rawString = "穥랑瞳Ⴆ䜷䉡";
        string alphaNumericEncodedString = AlphaNumeric.Data.EncodeString(rawString); // Output is ZXqRt7N3phA3R2FC
        string originalString = AlphaNumeric.Data.DecodeString(alphaNumericEncodedString);
    }

# Encode english
If you have english that is mostly alphanumeric you can escape all non-alphanumeric characters using the english-encoder.

    public void EncodeEnglish()
    {
        string email = "john.fakename@fakeemail.com";
        string alphaNumericEncodedString = AlphaNumeric.English.Encode(email); // john0Ufakename1fbfakeemail0Ucom
        string original = AlphaNumeric.English.Decode(alphaNumericEncodedString); // john.fakename@fakeemail.com
    }

