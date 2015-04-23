import org.jsoup.nodes.Document;
import org.jsoup.select.Elements;

import java.util.LinkedList;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

public class LinkChecker {
    private Document document;
    private String site;
    private String domainName;
    private Elements elements;
    private LinkedList<String> listOfLinks = new LinkedList<>();
    private final String tegA = "a";
    private final String attrHref = "href";

    public LinkChecker(Document document, String site) {
        this.document = document;
        this.site = site;
        domainName = getDomainName();
        getAllElementsByTeg(tegA);
        getAllLinks(attrHref);
        handlingLinks();
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
                forEach(System.out::println);
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
        if (link != "") {
            char firstSymbolOfLink = link.charAt(0);
            return (firstSymbolOfLink == '.' || firstSymbolOfLink == '/');
        }
        return false;
    }
}
