import javax.swing.text.html.HTML;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.HttpURLConnection;
import java.net.URL;

public class Spectator {
    private String site;
    private StringBuffer textOfSite = new StringBuffer();

    public Spectator() throws IOException {
        site = WebServiceAdapter.getURL();
        setTextOfSite();
    }

    private void setTextOfSite() throws IOException {
        URL url = new URL(site);
        HttpURLConnection httpURLConnection = (HttpURLConnection) url.openConnection();

        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(httpURLConnection.getInputStream()));

        String input;
        while ((input = bufferedReader.readLine()) != null) {
            textOfSite.append(input);
            System.out.println(HTML.Tag.A);
        }


    }

    public StringBuffer getTextOfSite() {
        return textOfSite;
    }
}