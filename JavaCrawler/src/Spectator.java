import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;

public class Spectator {
    private String site;
    private Document document;

    public Spectator() throws IOException {
        site = WebServiceAdapter.getURL();
        setDocument();
        getAllAttrsByTegAndAttr("a", "href");
    }

    private void setDocument() {
        try {
            document = Jsoup.connect(String.valueOf(site)).get();
        } catch (IOException e) {
            System.out.println("Не удаётся подключиться к сайту");
            e.printStackTrace();
        }
    }

    private void getAllAttrsByTegAndAttr(String teg, String attr) {
        Elements aElements = document.select(teg);
        for (Element aElement : aElements) {
            System.out.println(aElement.attr(attr));
        }
    }

}