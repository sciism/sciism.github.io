+++
title = "Mailcap"
date = "2022-04-20"
+++

## About

Mailcap files are a format documented in
[RFC 1524](https://www.rfc-editor.org/rfc/rfc1524.html), "A User Agent
Configuration Mechanism For Multimedia Mail Format Information." They allow the
handling of MIME types by software aware of those types. For example, a mailcap
line of `text/html; qutebrowser '%s'; test=test -n "$DISPLAY"` would instruct
the software to open any HTML file with qutebrowser if you are running a
graphical session, with the file replacing the `'%s'`.

`mailcap` is a parsing library that looks at either a present `$MAILCAPS` env
variable or cycles through the four paths where a mailcap file would be found in
ascending order of importance: `/usr/local/etc/mailcap`, `/usr/etc/mailcap`,
`/etc/mailcap`, and `$HOME/.mailcap`. It builds the mailcap from all available
files, with duplicate entries being squashed with newer lines, allowing
`$HOME/.mailcap` to be the final decider.

The entries that make up the mailcap include only those that are relevant i.e.
those that have passed the `test` field (if present). With the above `text/html`
example, that test would fail if run through SSH, and unless another existing
`text/html` entry (or `text/*`) exists that doesn't require a display server, no
entry would exist for that mime type.

## Installation

Add the following to your `Cargo.toml`:

```toml
[dependencies]
mailcap = "0.1.0"
```

## Usage

```rust
use mailcap::Mailcap;

fn main() {
    let cap = Mailcap::new().unwrap();
    if let Some(i) = cap.get("text/html") {
        let command = i.viewer("/var/www/index.html");
        assert_eq!(command, "qutebrowser '/var/www/index.html'");
    }
}
```

Wildcard fallbacks are also supported.

```rust
use mailcap::Mailcap;

fn main() {
    let cap = Mailcap::new().unwrap();
    if let Some(i) = cap.get("video/avi") {
        // if no video/avi MIME entry available
        let mime_type = i.mime();
        assert_eq!(mime_type, "video/*");
    }
}
```

Code documentation is [available here](https://docs.rs/mailcap/latest/mailcap/).

## Roadmap

To-be-included features can be seen
[through the current open issues](https://todo.sr.ht/~savoy/mailcap?search=status%3Aopen+label%3Afeature).

## Support

Refer to the
[announcement mailing list](https://lists.sr.ht/~savoy/mailcap-announce) for
project updates and the
[devel mailing list](https://lists.sr.ht/~savoy/mailcap-devel) for contributions
and collaboration.

Issues should be directed to the project
[issue tracker](https://todo.sr.ht/~savoy/mailcap).

## License

This project is licensed under the GPLv3.
