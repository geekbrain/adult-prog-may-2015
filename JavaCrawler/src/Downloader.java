import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;

import java.io.IOException;

public class Downloader {
    private String site;
    private Document document;

    public Downloader() throws IOException {
        site = WebServiceAdapter.getURL();
        setDocument();
    }

    private void setDocument() {
        try {
            document = Jsoup.connect(site).get();
        } catch (IOException e) {
            System.out.println("Не удаётся подключиться к сайту");
            e.printStackTrace();
        }
    }

    public Document getDocument() {
        return document;
    }

    public String getSite() {
        return site;
    }
}