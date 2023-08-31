using Furion.FriendlyException;

namespace JoyAdmin.Core;

[ErrorCodeType]
public enum ErrorCode
{
    [ErrorCodeItemMetadata("账号输入不正确", ErrorCode = 1001)]
    WrongUser,

    [ErrorCodeItemMetadata("密码输入不正确", ErrorCode = 1002)]
    WrongPwd,

    [ErrorCodeItemMetadata("原密码输入不正确", ErrorCode = 1003)]
    WrongOldPwd,

    [ErrorCodeItemMetadata("{0}", ErrorCode = 1004)]
    WrongValidation,


    [ErrorCodeItemMetadata("操作失败", ErrorCode = 1005)]
    Faild,

    [ErrorCodeItemMetadata("非法操作", ErrorCode = 1009)]
    Illegal
}

public static class ErrorStatus
{
    public static int ValidationFaild = 400;
}