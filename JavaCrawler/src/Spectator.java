import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;

public class Spectator {
    private String site;
    private Document document;
    private Elements elements;

    public Spectator() throws IOException {
        site = WebServiceAdapter.getURL();
        setDocument();
        getAllElementsByTeg("a");
        handlingHrefs("href");
    }

    private void setDocument() {
        try {
            document = Jsoup.connect(site).get();
        } catch (IOException e) {
            System.out.println("Не удаётся подключиться к сайту");
            e.printStackTrace();
        }
    }

    private Elements getAllElementsByTeg(String teg) {
        elements = document.select(teg);
        return elements;
    }

    private void handlingHrefs(String attr) {
        for (Element element : elements) {
            System.out.println(element.attr(attr));
        }
    }
}