function showMessage(message, priority, title) {
    $('#message').html("");
    $(document).trigger("add-alerts", [
      {
          'message': message,
          'priority': priority,
          'title': title
      }
    ]);
}