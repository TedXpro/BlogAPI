
class Error{
    public static Exception ErrEmailEmpty = new Exception("Email is required");
    public static Exception ErrEmailInvalid = new Exception("Email is invalid");
    public static Exception ErrPasswordEmpty = new Exception("Password is required");
    public static Exception ErrPasswordInvalid = new Exception("Password is invalid");
    public static Exception ErrUserNotFound = new Exception("User not found");
    public static Exception ErrUserExists = new Exception("User already exists");
    public static Exception ErrIncorrectEmailPassword = new Exception("Incorrect email or password");
}