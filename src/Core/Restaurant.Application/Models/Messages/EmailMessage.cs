namespace Restaurant.Application.Models.Messages
{
    public class EmailMessage
    {
        public string Subject { get; private set; } = string.Empty;
        public string Body { get; private set; } = string.Empty;

        /// <summary>
        /// Constructor mặc định cho Object Initializer.
        /// </summary>
        private EmailMessage() { }

        /// <summary>
        /// Email xác thực tài khoản (đăng ký).
        /// </summary>
        public EmailMessage(string userName, string verificationCode)
        {
            Subject = "Restaurant - Email Verification Code";
            Body = $"Hello {userName},<br/><br/>Your verification code is: <b>{verificationCode}</b><br/>This code will expire in 15 minutes.";
        }

        /// <summary>
        /// Email xác nhận yêu cầu đổi email (gửi tới current email).
        /// </summary>
        public static EmailMessage ForCurrentEmailConfirmation(string userName, string verificationCode)
        {
            return new EmailMessage
            {
                Subject = "Restaurant - Current Email Verification Code",
                Body = $"Hello {userName},<br/><br/>We received a request to change your email address. Your verification code is: <b>{verificationCode}</b><br/>If you did not request this, please ignore this email.<br/>This code will expire in 15 minutes."
            };
        }

        /// <summary>
        /// Email xác nhận địa chỉ email mới.
        /// </summary>
        public static EmailMessage ForEmailChange(string userName, string verificationCode)
        {
            return new EmailMessage
            {
                Subject = "Confirm your new email address",
                Body = $"Hello {userName},<br/><br/>"
                     + $"We received a request to change your email address.<br/>"
                     + $"Your verification code is: <b>{verificationCode}</b><br/>"
                     + $"This code will expire in 15 minutes.<br/><br/>"
                     + $"If you did not request this change, please ignore this email."
            };
        }


    }
}
