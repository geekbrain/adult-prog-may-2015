import org.jsoup.nodes.Document;
import org.jsoup.select.Elements;

import java.util.ArrayList;
import java.util.List;
import java.util.Objects;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import java.util.stream.Collectors;

public class LinkChecker {
    private Document document;
    private String site;
    private String domainName;
    private Elements elements;
    private List<String> listOfAllLinks = new ArrayList<>();
    private List<String> listOfGoodLinks = new ArrayList<>();
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
                forEach(element -> listOfAllLinks.add(element.attr(attr)));
    }

    private void handlingLinks() {
        listOfGoodLinks.addAll(listOfAllLinks.stream().
                filter(link -> isLinkDomainName(link) || isLinkLocal(link)).
                collect(Collectors.toList()));
    }

    private String getDomainName() {
        Pattern patStart = Pattern.compile("https?://");
        Pattern patEnd = Pattern.compile("/.*");
        Matcher mat = patStart.matcher(site);
        String siteWithoutStart = mat.replaceFirst("");
        mat = patEnd.matcher(siteWithoutStart);
        return mat.replaceFirst("");
    }

    private Boolean isLinkDomainName(String link) {
        Pattern pat = Pattern.compile(domainName);
        Matcher mat = pat.matcher(link);
        return mat.find();
    }

    private boolean isLinkLocal(String link) {
        if (!Objects.equals(link, "")) {
            char firstSymbolOfLink = link.charAt(0);
            return (firstSymbolOfLink == '.' || firstSymbolOfLink == '/');
        }
        return false;
    }

    public List<String> getListOfGoodLinks() {
        return listOfGoodLinks;
    }
}
