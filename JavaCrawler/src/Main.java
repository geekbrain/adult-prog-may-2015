import java.io.IOException;

public class Main {
    public static void main(String[] args) {
        try {
            Spectator spectator = new Spectator();
        } catch (IOException e) {
            e.printStackTrace();
        }
    }
}