namespace RunEmpire.Services;

public class GeoHashService
{
    public string Encode(double lat, double lng, int precision = 7)
    {
        const string base32 = "0123456789bcdefghjkmnpqrstuvwxyz";

        int[] bits = { 16, 8, 4, 2, 1 };

        double[] latRange = { -90.0, 90.0 };
        double[] lngRange = { -180.0, 180.0 };

        var geohash = new List<char>();

        bool isEven = true;
        int bit = 0;
        int ch = 0;

        while (geohash.Count < precision)
        {
            double mid;
            if (isEven)
            {
                mid = (lngRange[0] + lngRange[1]) / 2;
                if (lng > mid)
                {
                    ch |= bits[bit];
                    lngRange[0] = mid;
                }
                else lngRange[1] = mid;
            }
            else
            {
                mid = (latRange[0] + latRange[1]) / 2;
                if (lat > mid)
                {
                    ch |= bits[bit];
                    latRange[0] = mid;
                }
                else latRange[1] = mid;
            }

            isEven = !isEven;

            if (bit < 4) bit++;
            else
            {
                geohash.Add(base32[ch]);
                bit = 0;
                ch = 0;
            }
        }

        return new string(geohash.ToArray());
    }

}