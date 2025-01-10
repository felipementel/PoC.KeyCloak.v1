  # keycloak-service:
  #   container_name: keycloak-container
  #   build:
  #     context: . 
  #     dockerfile: ./keycloak.dockerfile
  #   image: felipementel/custom-keycloak:26.0.7
  #   environment:
  #     KC_BOOTSTRAP_ADMIN_USERNAME: admin
  #     KC_BOOTSTRAP_ADMIN_PASSWORD: ${KEYCLOAK_ADMIN_PASSWORD}
  #     KC_HOSTNAME: localhost
  #     KC_HOSTNAME_PORT: 8080
  #     KC_HOSTNAME_STRICT_BACKCHANNEL: 'true'
  #     KC_HOSTNAME_STRICT_FRONTCHANNEL: 'true'
  #     KC_HTTP_ENABLED: 'true' #PRD false
  #     KC_HEALTH_ENABLED: 'true'
  #     KC_METRICS_ENABLED: 'true'
  #     KC_HTTP_METRICS_HISTOGRAMS_ENABLED: 'true'
  #     KC_CACHE_METRICS_HISTOGRAMS_ENABLED: 'true'
  #     KC_LOG_LEVEL: INFO # DEBUG
  #     # DB Configuration
  #     KC_DB: postgres
  #     KC_DB_VENDOR: postgres
  #     KC_DB_SCHEMA: public
  #     KC_DB_HOST: postgresHost
  #     KC_DB_PORT: 5432
  #     KC_DB_NAME: keycloakDB
  #     KC_DB_URL: jdbc:postgresql://postgres-container:5432/keycloakDB
  #     KC_DB_USERNAME: userKeyCloak
  #     KC_DB_PASSWORD: ${POSTGRES_PASSWORD}
  #   healthcheck:
  #     test: ["CMD-SHELL", "exec 3<>/dev/tcp/127.0.0.1/8080;echo -e \"GET /health/ready HTTP/1.1\r\nhost: http://localhost\r\nConnection: close\r\n\r\n\" >&3;grep \"HTTP/1.1 200 OK\" <&3"]
  #     interval: 30s
  #     start_period: 1m
  #     timeout: 3s
  #     retries: 25
  #   command: ['start-dev', '--http-port', '8080']
  #   ports:
  #     - 8087:8080
  #     - 7447:7443
  #     - 9007:9000
  #   networks:
  #     - local_network
  #   labels:
  #     - maintainer="Felipe Augusto, Canal DEPLOY"
  #   deploy:
  #     resources:
  #       limits:
  #         cpus: '0.50'
  #         memory: 256M
  #       reservations:
  #         cpus: '0.25'
  #         memory: 128M
  #   cpuset: '1'