# Создание задач 4–14 в GitHub Issues
# Запуск: из папки репозитория, после `gh auth login`
#   powershell -ExecutionPolicy Bypass -File .\create_issues.ps1

$A = "@me"            # участник А (вы)
$B = "LOGIN_B"        # <-- логин участника Б на GitHub

# Метки (если уже есть — просто будет предупреждение)
gh label create server --color 1f5fbf --description "Сервер Snake"   2>$null
gh label create client --color b5531a --description "Клиент SnakeWPF" 2>$null
gh label create docs   --color 2e7d32 --description "Отчёт, тесты"    2>$null

$tasks = @(
  @{ t="Задача 4. Сервер: метод Send";                                   s="8";     a=$A; l="server" },
  @{ t="Задача 5. Сервер: метод Receiver (порт 5001, команды)";          s="9";     a=$A; l="server" },
  @{ t="Задача 6. Сервер: метод AddSnake";                               s="10";    a=$A; l="server" },
  @{ t="Задача 7. Сервер: метод Timer (движение, столкновения)";         s="11";    a=$A; l="server" },
  @{ t="Задача 8. Сервер: SaveLeaders, LoadLeaders, потоки в Main";      s="12–14"; a=$A; l="server" },
  @{ t="Задача 9. Проект SnakeWPF, папки Image и Pages";                 s="15–17"; a=$B; l="client" },
  @{ t="Задача 10. Вёрстка страниц Home, Game, EndGame";                 s="18–20"; a=$B; l="client" },
  @{ t="Задача 11. MainWindow: переменные, StartReceiver, OpenPage, Receiver"; s="21–25"; a=$B; l="client" },
  @{ t="Задача 12. MainWindow: Send, EventKeyUp, QuitApplication, конструктор"; s="26–29"; a=$B; l="client" },
  @{ t="Задача 13. Логика страниц: StartGame, CreateUI, EndGame";        s="30–34"; a=$B; l="client" },
  @{ t="Задача 14. Тестирование и отчёт";                                s="35";    a="$A,$B"; l="docs" }
)

foreach ($x in $tasks) {
  gh issue create --title $x.t --body "Шаги методички: $($x.s)" --assignee $x.a --label $x.l
}
