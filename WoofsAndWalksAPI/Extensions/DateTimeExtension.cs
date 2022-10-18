namespace WoofsAndWalksAPI.Extensions;

public static class DateTimeExtension
{
    public static int CalculateAge(this DateTime dob)
    {
        var today = DateTime.Today;
        var age = today.Year - dob.Year;

        // check if they have had their birthday already and adjust
        if (dob.Date > today.AddYears(-age)) age--;
        return age;
    }
}
