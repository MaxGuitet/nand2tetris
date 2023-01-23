internal class JackSyntaxException : Exception
{
    public JackSyntaxException()
    {

    }

    public JackSyntaxException(string message) : base(message)
    {

    }

    public JackSyntaxException(string message, Exception inner) : base(message, inner)
    {

    }

}