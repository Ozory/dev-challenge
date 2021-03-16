const Request = window.Request
const Headers = window.Headers
const fetch = window.fetch

class Api {
  async request(method, url, body) {
    if (body) {
      body = JSON.stringify(body)
    }

    const request = new Request('/api/' + url, {
      method: method,
      body: body,
      credentials: 'same-origin',
      headers: new Headers({
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      })
    })

    const resp = await fetch(request)
    if (!resp.ok && resp.status !== 400) {
      throw Error(resp.statusText)
    }

    const jsonResult = await resp.json()

    if (resp.status === 400) {
      jsonResult.requestStatus = 400
    }

    return jsonResult
  }

  async getDomain(domainOrIp) {
    return this.request('GET', `domain/${domainOrIp}`)
  }
}

const api = new Api()

var callback = () => {
  const btn = document.getElementById('btn-search')
  const txt = document.getElementById('txt-search')
  const result = document.getElementById('whois-results');
  const message = document.getElementById('whois-message');
  const notfound = document.getElementById('whois-notfound');
  const searching = document.getElementById('whois-searching');

  if (btn) {
    btn.onclick = async () => {

      message.style.display = "none";
      notfound.style.display = "none";
      result.style.display = "none";

      var ipformatIP = /^(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
      var domainName = /^[a-zA-Z0-9][a-zA-Z0-9-]{1,61}[a-zA-Z0-9](?:\.[a-zA-Z]{2,})+$/;

      if (!txt.value.match(ipformatIP) && !txt.value.match(domainName)) {
        message.style.display = "block";
        return;
      }

      searching.style.display = "block";

      const response = await api.getDomain(txt.value);
      if (response) {
        var data = response;
        if (data && data.valid) {
          result.style.display = "block";
          searching.style.display = "none";

          const nm = document.getElementById('w-name');
          const hst = document.getElementById('w-hostedAt');
          const ip = document.getElementById('w-ip');

          nm.innerHTML = data.name;
          hst.innerHTML = data.hostedAt;
          ip.innerHTML = data.ip;
        } else {
          notfound.style.display = "block";
        }
      }

      searching.style.display = "none";
    }
  }
}

if (document.readyState === 'complete' || (document.readyState !== 'loading' && !document.documentElement.doScroll)) {
  callback()
} else {
  document.addEventListener('DOMContentLoaded', callback)
}
