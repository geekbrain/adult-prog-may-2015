import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.util.LinkedList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class Spectator {
    private String site;
    private String domainName;
    private Document document;
    private Elements elements;
    private LinkedList<String> listOfLinks = new LinkedList<>();
    private final String tegA = "a";
    private final String attrHref = "href";

    public Spectator() throws IOException {
        site = WebServiceAdapter.getURL();
        domainName = getDomainName();
        setDocument();
        getAllElementsByTeg(tegA);
        getAllLinks(attrHref);
        handlingLinks();
    }

    private void setDocument() {
        try {
            document = Jsoup.connect(site).get();
        } catch (IOException e) {
            System.out.println("Не удаётся подключиться к сайту");
            e.printStackTrace();
        }
    }

    private void getAllElementsByTeg(String teg) {
        elements = document.select(teg);
    }

    private void getAllLinks(String attr) {
        elements.stream().
                forEach(element -> listOfLinks.add(element.attr(attr)));
    }

    private void handlingLinks() {
        listOfLinks.stream().
                filter(element -> checkLinkByDomainName(element) || checkLinkAsRelativeLocal(element)).
                forEach(element -> System.out.println(element));
    }

    private String getDomainName() {
        Pattern patStart = Pattern.compile("https?://");
        Pattern patEnd = Pattern.compile("/.*");
        Matcher mat = patStart.matcher(site);
        String siteWithoutStart = mat.replaceFirst("");
        mat = patEnd.matcher(siteWithoutStart);
        return mat.replaceFirst("");
    }

    private Boolean checkLinkByDomainName(String link) {
        Pattern pat = Pattern.compile(domainName);
        Matcher mat = pat.matcher(link);
        return mat.find();
    }

    private boolean checkLinkAsRelativeLocal(String link) {
        char firstSymbolOfLink = link.charAt(0);
        return (firstSymbolOfLink == '.' || firstSymbolOfLink == '/');
    }
}