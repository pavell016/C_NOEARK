<table>
  <tr>
    <th>SERVIDOR</th>
    <th>CLIENT</th>
  </tr>
  <tr>
    <td colspan="2">
      <strong>#definir port/IP</strong><br>
      <code>IPAddress ServerIPAddress = IPAddress.Parse("<IP>");</code><br>
      <code>int ServerPort = 5000;</code><br>
      <code>IPEndPoint ip_end_point = new IPEndPoint(ServerIPAddress, ServerPort);</code>
    </td>
  </tr>
  <tr>
    <td><strong>#Definir un Servidor</strong></td>
    <td><strong>#Definir un Client</strong></td>
  </tr>
  <tr>
    <td><code>TcpListener SERVIDOR = new TcpListener(ip_end_point);</code></td>
    <td><code>TcpClient CLIENT = new TcpClient();</code></td>
  </tr>
    <tr>
    <td><strong>#Engegar el Servidor</strong></td>
    <td><strong>#Connexió amb el Client</strong></td>
  </tr>
  <tr>
    <td><code>SERVIDOR.start();</code></td>
    <td><code>CLIENT.connect(ip_end_point);</code></td>
  </tr>
    </tr>
    <tr>
    <td><strong>#acceptar client</strong></td>
    <td><strong></strong></td>
  </tr>
  <tr>
    <td><code>TcpClient client_a_acceptar = SERVIDOR.AcceptTcpClient();</code></td>
    <td><code></code></td>
  </tr>
  </tr>
  <tr>
    <td colspan="2">
      <strong>#definir el fluxe de xara</strong><br>
      <code>NetworkStream ns = client.GetStream();</code>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <strong>#Lectura de flux</strong><br>
      <code>byte[] bufferlocal = new Byte[1024];// buffer is to store an encoded message</code><br>
      <code>ns.Read(bufferlocal, 0, bufferlocal.length);</code><br>
      <code>String missatge = Encoding.<Type of encoding>UTF8.GetString(bufferlocal.ToArray, 0, bufferlocal.length);</code><br>
    </td>
  </tr>
  <tr>
    <td colspan="2">
      <strong>#Lectura recursiva de flux</strong><br>
      <code>byte[] bufferlocal = new Byte[1024];// buffer is to store an encoded message</code><br>
      <code>MemoryStream bufferalldata = new MemoryStream();</code><br>
      <code>String missatgeRecursiu = ns.Read(bufferlocal, 0, bufferlocal.length);</code><br>
      <code>bufferalldata.Write(bufferlocal, 0, bufferlocal.length);</code><br>
      <code>while (MyNetworkStream.DataAvailable) {<br>
      missatgeRecursiu  += ns.Read(bufferlocal, 0, bufferlocal.Length);<br>
             bufferalldata.Write(BufferLocal, 0, BufferLocal.Length);<br>
        }
      </code>
    </td>
  </tr>
        <tr>
    <td colspan="2">
      <strong>#escriure al flux</strong><br>
      <code>byte[] msg;</code><br>
      <code>msg = Encoding.<Type of encoding>UTF8.GetBytes(missatge);</code><br>
      <code>MyNetworkStream.Write(msg, 0, msg.Length);</code><br>
    </td>
  </tr>
  </tr>
        <tr>
    <td colspan="2">
      <strong>#tancar flux i client</strong><br>
      <code>ns.close();</code><br>
      <code>CLIENT.close();</code><br>
    </td>
  </tr>      
</table>
