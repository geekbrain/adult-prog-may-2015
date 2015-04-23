import java.io.IOException;

public class Main {
    public static void main(String[] args) {
        try {
            Downloader downloader = new Downloader();
            LinkChecker linkChecker = new LinkChecker(downloader.getDocument(), downloader.getSite());
            linkChecker.getListOfGoodLinks();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}