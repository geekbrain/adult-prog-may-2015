import com.sun.org.apache.xpath.internal.operations.Bool;
import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

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
            checkLink(element.attr(attr));
        }
    }

    private void checkLink(String link) {
        Pattern pat = Pattern.compile("lenta");
        Matcher mat = pat.matcher(link);
        Boolean found;

        System.out.println(link);
        found = mat.find();

        System.out.println(found);
    }
}