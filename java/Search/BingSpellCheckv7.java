import java.io.*;
import java.net.*;
import javax.net.ssl.HttpsURLConnection;

public class HelloWorld {

    // Add your Bing Spell Check subscription key to your environment variables.
    static String host = System.getenv("BING_SPELL_CHECK_ENDPOINT");
    static String path = "/bing/v7.0/spellcheck";

    // Add your Bing Spell Check endpoint to your environment variables.
    static String key = System.getenv("BING_SPELL_CHECK_SUBSCRIPTION_KEY");

    static String mkt = "en-US";
    static String mode = "proof";
    static String text = "Hollo, wrld!";

    public static void check () throws Exception {
		String params = "?mkt=" + mkt + "&mode=" + mode;
        URL url = new URL(host + path + params);
        HttpsURLConnection connection = (HttpsURLConnection) url.openConnection();
        connection.setRequestMethod("POST");
        connection.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
		connection.setRequestProperty("Content-Length", "" + text.length() + 5);
        connection.setRequestProperty("Ocp-Apim-Subscription-Key", key);
        connection.setDoOutput(true);

        DataOutputStream wr = new DataOutputStream(connection.getOutputStream());
        wr.writeBytes("text=" + text);
        wr.flush();
        wr.close();

        BufferedReader in = new BufferedReader(
        new InputStreamReader(connection.getInputStream()));
        String line;
        while ((line = in.readLine()) != null) {
            System.out.println(line);
        }
        in.close();
    }

    public static void main(String[] args) {
        try {
            check ();
        }
        catch (Exception e) {
            System.out.println (e);
        }
    }
}
