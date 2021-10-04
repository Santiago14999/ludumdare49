using UnityEngine;

public static class InputExtension
{
    public static KeyCode FirstLetterDown()
    {
        for (int i = 97; i < 123; i++)
        {
            if (Input.GetKeyDown((KeyCode) i))
            {
                return (KeyCode) i;
            }
        }

        return KeyCode.None;
    }

    public static bool AnyKeyDown()
    {
        return FirstLetterDown() != KeyCode.None
               || Input.GetKeyDown(KeyCode.Space)
               || Input.GetKeyDown(KeyCode.Mouse0)
               || Input.GetKeyDown(KeyCode.Mouse1)
               || Input.GetKeyDown(KeyCode.Return);
    }

    public static KeyCode FirstNumberDown()
    {
        for (int i = 48; i < 58; i++)
        {
            if (Input.GetKeyDown((KeyCode) i))
            {
                return (KeyCode) i;
            }
        }
        
        return KeyCode.None;
    }
}
