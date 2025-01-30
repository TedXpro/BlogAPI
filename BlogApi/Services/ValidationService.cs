public class ValidationService{
    public static Exception ValidatePassword(string password){
        if (password == null || password == ""){
            return Error.ErrPasswordEmpty;
        }

        int upper = 0, lower = 0, digit = 0, special = 0;
        for (int i = 0; i < password.Length; i++){
            if (password[i] >= 'A' && password[i] <= 'Z'){
                upper++;
            }
            else if (password[i] >= 'a' && password[i] <= 'z'){
                lower++;
            }
            else if (password[i] >= '0' && password[i] <= '9'){
                digit++;
            }
            else{
                special++;
            }
        }

        if (upper == 0 || lower == 0 || special == 0 || digit == 0){
            return Error.ErrPasswordInvalid;
        }

        return Error.noError;
    }
}