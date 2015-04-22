import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Spectator {
    private String site;
    private String domainName;
    private Document document;
    private Elements elements;

    public Spectator() throws IOException {
        site = WebServiceAdapter.getURL();
        domainName = getDomainName();
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
            if (checkLinkByDomainName(element.attr(attr))) System.out.println(element.attr(attr));
        }
    }

    private String getDomainName() {
        Pattern patStart = Pattern.compile("https?://");
        Pattern patEnd = Pattern.compile("/.*");
        Matcher mat = patStart.matcher(site);
        String siteWithoutStart = mat.replaceFirst("");
        mat = patEnd.matcher(siteWithoutStart);
        String domainName = mat.replaceFirst("");
        return domainName;
    }

    private Boolean checkLinkByDomainName(String link) {
        Pattern pat = Pattern.compile(domainName);
        Matcher mat = pat.matcher(link);
        Boolean found = mat.find();
        return found;
    }
}