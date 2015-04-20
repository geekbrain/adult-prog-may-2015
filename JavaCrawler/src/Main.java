import java.io.IOException;

/**
 * Created by Андрей on 14.04.2015.
 */
public class Main {
    public static void main(String[] args) {
        try {
            Spectator spectator = new Spectator();
            System.out.println(spectator.getTextOfSite());
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}
